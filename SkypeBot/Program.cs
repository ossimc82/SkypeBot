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
        public static List<string> _users;
        public static TUserStatus curStatus;

        static void Main(string[] args)
        {
            _users = new List<string>();
            skype = new Skype();

            if (!skype.Client.IsRunning)
            {
                skype.Client.Start(true, true);
                Writer.WriteErrorln("[" + DateTime.Now + "] SkypeClient not running, the programm will start it now.");
                System.Threading.Thread.Sleep(5000);
                while (skype.CurrentUserStatus == TUserStatus.cusLoggedOut) { System.Threading.Thread.Sleep(5000); }
            }
            FileController.CheckFiles();
            Writer.WriteSuccessln("[" + DateTime.Now + "] Loading...");
            Initialize:
            try
            {
                //Loads events
                skype.Attach();
                //Listen 
                skype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);
                curStatus = skype.CurrentUserStatus;
                UserList.GetContacts();
                UserList.LoadIgnoreList();
            }
            catch
            {
                Writer.WriteErrorln("Load Error, Retrying...");
                System.Threading.Thread.Sleep(100);
                goto Initialize;
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
            if (status == TChatMessageStatus.cmsReceived)
            {
                if (msg.Chat.Members.Count > 2)
                {
                    if (!UserList.IsChatIgnored(msg))
                    {
                        try
                        {
                            if (msg.Body.StartsWith("!say "))
                            {
                                msg.Chat.SendMessage(msg.Body.Replace("!say ", String.Empty));
                            }
                            else
                            {
                                //Send processed message back to skype chat window
                                msg.Chat.SendMessage(CommandProcessor.ProcessCommand(msg.Body, msg));
                            }
                        }
                        catch (Exception ex)
                        {
                            Writer.WriteErrorln(ex.ToString());
                        }
                        //When you get a message
                        Writer.WriteGetChat("Get chat: [" + DateTime.Now + "] " + "[" + msg.Chat.Name + ", " + msg.Chat.FriendlyName + "]: ");
                        Console.Write(msg.Body + "\n\r");

                        //When the bot sends the ressult
                        Writer.WriteSuccess("Send Chat: [" + DateTime.Now + "] " + "To [" + msg.Chat.Name + ", " + msg.Chat.FriendlyName + "]: ");
                        Console.Write(CommandProcessor.ProcessCommand(msg.Body, msg) + "\n\r");
                    }
                    else { }
                }
                else
                {
                    if (_users.Contains(msg.Sender.Handle))
                    {
                        if (!UserList.IsUserIgnored(msg))
                        {
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
                        else { }
                    }
                    else
                    {
                        Writer.WriteWarningln("[" + DateTime.Now + "] Blocked sending message to: " + msg.Sender.Handle);
                    }
                }
            }
            //if (status == TChatMessageStatus.cmsSending)
            //{
            //    if (msg.Body.StartsWith("!say "))
            //    {
            //        msg.Chat.SendMessage(msg.Body.Replace("!say ", String.Empty));
            //    }
            //    else
            //    {
            //        //Send processed message back to skype chat window
            //        msg.Chat.SendMessage(CommandProcessor.ProcessCommand(msg.Body, msg));
            //    }
            //}
        }
    }
}
