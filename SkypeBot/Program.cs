using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SKYPE4COMLib;
using WMPLib;

namespace SkypeBot
{
    class Program
    {
        static Skype skype;
        static WindowsMediaPlayer mediaPlayer;
        static string[] ignoredUsers;
        static TUserStatus curStatus;

        static void Main(string[] args)
        {
            skype = new Skype();

            if (!skype.Client.IsRunning)
                skype.Client.Start(true, true);
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot");
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers"))
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers").Close();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[" + DateTime.Now + "] Loading...");
            Console.ResetColor();
            curStatus = skype.CurrentUserStatus;

            //Loads events
            skype.Attach();
            //Listen 
            skype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);
            skype.UserStatus += new _ISkypeEvents_UserStatusEventHandler(skype_UserStatus);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[" + DateTime.Now + "] Loading complete...");
            Console.Beep();
            Console.ResetColor();

            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Terminating...");
                System.Threading.Thread.Sleep(100);
                Environment.Exit(0);
            };

            while (true) { Console.Read(); }
        }

        private static void skype_UserStatus(TUserStatus Status)
        {
            if (Status != TUserStatus.cusOffline || Status != TUserStatus.cusInvisible || Status != TUserStatus.cusSkypeMe || Status != TUserStatus.cusUnknown || Status != TUserStatus.cusNotAvailable)
                curStatus = Status;
        }

        static void skype_MessageStatus(ChatMessage msg, TChatMessageStatus status)
        {
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
                            if (!File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers").Contains("ficken" + ","))
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
                    //Send processed message back to skype chat window
                    skype.SendMessage(msg.Sender.Handle, ProcessCommand(msg.Body, msg));
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
                Console.Write(ProcessCommand(msg.Body, msg) + "\n\r");
            skipSendMSG: { }
            }
        }

        static string ProcessCommand(string str, ChatMessage message)
        {
            string result;
            //Here are the the words that the bot understands.
            switch (str)
            {
                case "go offline!":
                    {
                        //you will go back online after 5 sec
                        skype.ChangeUserStatus(TUserStatus.cusOffline);
                        System.Threading.Thread.Sleep(5000);
                        skype.ChangeUserStatus(curStatus);
                        result = "Back :)";
                    } break;
                case "hello":
                    result = "Hello!";
                    break;
                case "!help":
                    result = "go offline!, hello, !help, date, time, who, who am i?, penis, your mother, hi, wake him up!!!, contactsamount, !ignore, !unignore";
                    break;
                case "date":
                    result = "Current Date is: " + DateTime.Now.ToLongDateString();
                    break;
                case "time":
                    result = "Current Time is: " + DateTime.Now.ToLongTimeString();
                    break;
                case "who":
                    result = "You write with a skype bot, enjoy";
                    break;
                case "who am i?":
                    result = "you are " + message.Sender.Handle + " and your Fullname is " + message.Sender.FullName;
                    break;
                case "penis":
                    result = "vagina";
                    break;
                case "your mother":
                    result = "your fish";
                    break;
                case "hi":
                    result = "hey :)";
                    break;
                case "wake him up!!!":
                    {
                        mediaPlayer = new WindowsMediaPlayer();
                        mediaPlayer.URL = "http://countersossi.co.funpic.de/rest/Linkin%20Park%20-Leave%20out%20all%20the%20rest%20-%20Lyrics.mp3";
                        mediaPlayer.controls.play();
                        result = "Let us wake up this asshole, I play music for him :)";
                    } break;
                case "contactsamount":
                    result = "You have " + message.Sender.NumberOfAuthBuddies + " contacts.";
                    break;
                case "!ignore":
                    {
                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers", true))
                        {
                            writer.Write(message.Sender.Handle + ",");
                        }
                        result = "You (" + message.Sender.Handle + ") dont get message from me now, you can get messages with \"!unignore\".";
                    } break;
                case "!unignore":
                    {
                        string name = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers");
                        if (!name.Contains(message.Sender.Handle + ","))
                            result = "Sorry but you (" + message.Sender.Handle + ") are not on my ignore list, with \"!ignore\" I'll not contact you again.";
                        else
                            result = "An error ocured while executing the command.";
                    } break;
                default:
                    result = "Sorry, I do not recognize your command. Type \"!help\" to get a list of all commands. You can disable me with \"!ignore\"";
                    break;
            }
            return result;
        }
    }

    //public static class StringAdd
    //{
    //    static Skype skype;
    //    public static string ExecuteVoid(this string skypename, string )
    //    {
    //        return null;
    //    }
    //}
}
