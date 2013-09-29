using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SKYPE4COMLib;

namespace SkypeBot
{
    class Program
    {
        private static Skype skype;
        private static string config;
        private static string[] ignoredUsers;
        //private static CommandProcessor cmd;
        public static TUserStatus curStatus;

        static void Main(string[] args)
        {
            skype = new Skype();

            if (!skype.Client.IsRunning)
            {
                skype.Client.Start(true, true);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[" + DateTime.Now + "] SkypeClient not running, the programm will start it now and wait 10 seconds untill the client is started.");
                Console.ResetColor();
                System.Threading.Thread.Sleep(10000);
            }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot");
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers"))
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers").Close();
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\config.cfg"))
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\config.cfg").Close();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[" + DateTime.Now + "] Loading...");
            Console.ResetColor();
            curStatus = skype.CurrentUserStatus;

            //Loads config file
            //TODO: Implement it when WindowsForm is set up.
            config = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\config.cfg");

            //Loads events
            skype.Attach();
            //Listen 
            skype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[" + DateTime.Now + "] Loading complete...");
            Console.Beep();
            Console.ResetColor();

            Console.CancelKeyPress += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Terminating...");
                System.Threading.Thread.Sleep(100);
                Environment.Exit(0);
            };

            while (true) { Console.Read(); }
        }

        static void skype_MessageStatus(ChatMessage msg, TChatMessageStatus status)
        {
            //cmd = new CommandProcessor();
            ignoredUsers = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers").Split(',');

            if (status == TChatMessageStatus.cmsReceived)
            {
                //first look if the user is ignored
                for (int i = 0; i < ignoredUsers.Length; i++)
                {
                    if (msg.Sender.Handle == ignoredUsers[i])
                    {
                        if (msg.Body == "!unignore")
                        {
                            if (!File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers").Contains(msg.Sender.Handle + ","))
                                skype.SendMessage(msg.Sender.Handle, "Sorry but you are not on my ignore list, with \"!ignore\" I'll not contact you again.");
                            else
                                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers", File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers").Replace(msg.Sender.Handle + ",", String.Empty));
                        }
                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\skypelog.log", true))
                        {
                            writer.WriteLine("[" + DateTime.Now + "] " + "[" + msg.Sender.Handle + ", " + msg.Sender.FullName + "]: " + msg.Body);
                        }

                        //When you get a message from an ignored user
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("Get IgnoredUser chat: [" + DateTime.Now + "] " + "[" + msg.Sender.Handle + ", " + msg.Sender.FullName + "]: ");
                        Console.ResetColor();
                        Console.Write(msg.Body + "\n\r");

                        goto skipSendMSG;
                    }
                }
                try
                {
                    if (msg.Body.StartsWith("!say "))
                    {
                        skype.SendMessage(msg.Sender.Handle, msg.Body.Replace("!say ", String.Empty));
                    }
                    else
                    {
                        //Send processed message back to skype chat window
                        skype.SendMessage(msg.Sender.Handle, CommandProcessor.ProcessCommand(msg.Body, msg));
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.ToString());
                    Console.ResetColor();
                }
                //When you get a message
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Get chat: [" + DateTime.Now + "] " + "[" + msg.Sender.Handle + ", " + msg.Sender.FullName + "]: ");
                Console.ResetColor();
                Console.Write(msg.Body + "\n\r");

                //When the bot sends the ressult
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Send Chat: [" + DateTime.Now + "] " + "To [" + msg.Sender.Handle + ", " + msg.Sender.FullName + "]: ");
                Console.ResetColor();
                Console.Write(CommandProcessor.ProcessCommand(msg.Body, msg) + "\n\r");
            }
        skipSendMSG: { }
        }
    }
}
