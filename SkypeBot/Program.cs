﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SKYPE4COMLib;
using SkypeBot.Handlers;
using System.Windows.Forms;
using System.Threading;
using SkypeBot.Forms;

namespace SkypeBot
{
    class Program
    {
        private static Skype skype;
        public static List<string> _users;
        public static TUserStatus curStatus;
        public static Thread t;

        static void Main(string[] args)
        {
            t = new Thread(() => new UserController().ShowDialog());
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
                UserListHandler.GetContacts();
                UserListHandler.LoadIgnoreList();
                skype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);
                curStatus = skype.CurrentUserStatus;
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

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            
            t.Start();

            while (true) 
            {
                string input = Console.ReadLine();
                Writer.WriteSuccess(ConsoleCommandHandler.ProcessCommand(input));

            }
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
