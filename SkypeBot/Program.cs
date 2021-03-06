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
        public static List<string> _users;          //Showed name in the skype
        public static List<string> _usernames;      //skypename
        public static string[,] UsersArray;
        public static TUserStatus curStatus;
        public static List<string> _FriendlyName;
        public static List<string> _Name;

        static void Main(string[] args)
        {
            _users = new List<string>();
            _usernames = new List<string>();
            skype = new Skype();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            Writer.WriteSuccessln("[" + DateTime.Now + "] Loading...");
            if (!skype.Client.IsRunning)
            {
                skype.Client.Start(true, true);
                Writer.WriteErrorln("[" + DateTime.Now + "] SkypeClient not running, the programm will start it now...");
                System.Threading.Thread.Sleep(12000);
                while (skype.CurrentUserStatus == TUserStatus.cusLoggedOut) { System.Threading.Thread.Sleep(5000); Writer.WriteErrorln("Not logged in... Retrying"); }
            }

            Writer.WriteWarningln("[" + DateTime.Now + "] Checking Files...");
            FileHandler.CheckFiles();
            Writer.WriteSuccessln("[" + DateTime.Now + "] Filecheck Finished...");

            Initialize:
            try
            {
                Writer.WriteWarningln("[" + DateTime.Now + "] Connecting to Skype...");
                //Loads events
                skype.Attach();
                Writer.WriteSuccessln("[" + DateTime.Now + "] Connected...");

                Writer.WriteWarningln("[" + DateTime.Now + "] Loading Contacts...");
                UserListHandler.GetContacts();
                UsersArray = new string[_users.Count,2];
                for (int i = 0; i < UsersArray.Length/2; i++)
                {
                    UsersArray[i, 0] = _users[i];         //ShowedName
                    UsersArray[i, 1] = _usernames[i];     //skypename
                }
                Writer.WriteSuccessln("[" + DateTime.Now + "] Loaded " + _users.Count + " contacts...");

                Writer.WriteWarningln("[" + DateTime.Now + "] Loading Recent GroupChats...");
                Writer.WriteErrorln("[" + DateTime.Now + "] this *CAN* take up to 5 minutes...");
                UserListHandler.GetGroupChats();
                Writer.WriteSuccessln("[" + DateTime.Now + "] Recent GroupChats Loaded...");

                Writer.WriteWarningln("[" + DateTime.Now + "] Loading Ignorelist...");
                UserListHandler.LoadIgnoreList();
                Writer.WriteSuccessln("[" + DateTime.Now + "] Ignorelist Loaded...");

                //Listen
                Writer.WriteWarningln("[" + DateTime.Now + "] Starting Messagelistener...");
                skype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);
                skype.OnlineStatus += new _ISkypeEvents_OnlineStatusEventHandler(skype_OnlineStatus);
                curStatus = skype.CurrentUserStatus;
                Writer.WriteSuccessln("[" + DateTime.Now + "] Messagelistener Started...");
            }
            catch
            {
                Writer.WriteErrorln("Load Error, Retrying...");
                System.Threading.Thread.Sleep(100);
                goto Initialize;
            }
            new Thread(() => new UserController().ShowDialog()).Start();
            Writer.WriteSuccessln("[" + DateTime.Now + "] Loading complete...");
            Console.Beep();

            Console.CancelKeyPress += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Terminating...");
                System.Threading.Thread.Sleep(100);
                Environment.Exit(0);
            };

            while (true) 
            {
                string input = Console.ReadLine();
                Writer.WriteSuccess(ConsoleCommandHandler.ProcessCommand(input));
            }
        }

        static void skype_OnlineStatus(User pUser, TOnlineStatus Status)
        {
            UserListHandler.UpdateOnlineStatus(pUser, skype.CurrentUser, Status, UserController.pictureBox1);
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
        }
    }
}
