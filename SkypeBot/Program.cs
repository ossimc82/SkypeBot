using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SKYPE4COMLib;
using SkypeBot.Handlers;

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
                System.Threading.Thread.Sleep(12000);
                while (skype.CurrentUserStatus == TUserStatus.cusLoggedOut) { System.Threading.Thread.Sleep(5000); Writer.WriteErrorln("Not logged in... Retrying"); }
            }
            FileHandler.CheckFiles();
            Writer.WriteSuccessln("[" + DateTime.Now + "] Loading...");
            Initialize:
            try
            {
                //Loads events
                skype.Attach();
                //Listen 
                skype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);
                curStatus = skype.CurrentUserStatus;
                UserListHandler.GetContacts();
                UserListHandler.LoadIgnoreList();
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
                if (!UserListHandler.IsChatIgnored(msg))
                {
                    ChatHandler.HandleChat(msg);
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
