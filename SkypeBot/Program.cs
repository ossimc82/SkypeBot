﻿using System;
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
        private static string[] ignoredUsers;
        private static List<string> _users = new List<string>();
        public static TUserStatus curStatus;

        static void Main(string[] args)
        {
            skype = new Skype();

            if (!skype.Client.IsRunning)
            {
                skype.Client.Start(true, true);
                Writer.WriteErrorln("[" + DateTime.Now + "] SkypeClient not running, the programm will start it now.");
                System.Threading.Thread.Sleep(5000);
                while (skype.CurrentUserStatus == TUserStatus.cusLoggedOut) { System.Threading.Thread.Sleep(5000); }
            }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot");
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers"))
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers").Close();

            Writer.WriteSuccessln("[" + DateTime.Now + "] Loading...");

            //Loads events
            skype.Attach();
            //Listen 
            skype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);
            curStatus = skype.CurrentUserStatus;

            for (int i = 0; i < skype.HardwiredGroups.Count; i++)
            {
                if (skype.HardwiredGroups[i + 1].Type == TGroupType.grpSkypeFriends)
                {
                    for (int j = skype.HardwiredGroups[i + 1].Users.Count; j > 0; j--)
                    {
                        _users.Add(skype.HardwiredGroups[i + 1].Users[j].Handle);
                    } break;
                }
            }

            Writer.WriteSuccessln("[" + DateTime.Now + "] Loaded " + _users.Count + " contacts.");

            Writer.WriteSuccessln("[" + DateTime.Now + "] Loading complete...");
            Console.Beep();

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
            ignoredUsers = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers").Split(',');
            if (status == TChatMessageStatus.cmsReceived)
            {
                if (_users.Contains(msg.Sender.Handle))
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
                            Writer.WriteIgnored("Get IgnoredUser chat: [" + DateTime.Now + "] " + "[" + msg.Sender.Handle + ", " + msg.Sender.FullName + "]: ");
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
                        Writer.WriteErrorln(ex.ToString());
                    }
                    //When you get a message
                    Writer.WriteGetChat("Get chat: [" + DateTime.Now + "] " + "[" + msg.Sender.Handle + ", " + msg.Sender.FullName + "]: ");
                    Console.Write(msg.Body + "\n\r");

                    //When the bot sends the ressult
                    Writer.WriteSuccess("Send Chat: [" + DateTime.Now + "] " + "To [" + msg.Sender.Handle + ", " + msg.Sender.FullName + "]: ");
                    Console.Write(CommandProcessor.ProcessCommand(msg.Body, msg) + "\n\r");
                }
                else
                {
                    Writer.WriteWarningln("[" + DateTime.Now + "] Blocked sending message to: " + msg.Sender.Handle);
                }
            }
        skipSendMSG: { }
        }
    }
}
