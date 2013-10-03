﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;
using System.IO;

namespace SkypeBot.Handlers
{
    public static class UserListHandler
    {
        public static List<string> Chats;
        private static string[] ignoredChats;
        static Skype skype;

        public static void LoadIgnoreList()
        {
            Chats = new List<string>();
            ignoredChats = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats").Split(',');

            for (int i = 0; i < ignoredChats.Length; i++)
            {
                Chats.Add(ignoredChats[i]);
            }
        }

        public static bool IsChatIgnored(ChatMessage msg)
        {
            if(Chats.Contains(msg.Chat.Name))
            {
                if (msg.Body.Equals("!unignore", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!Chats.Contains(msg.Chat.Name))
                        msg.Chat.SendMessage("Sorry but this chat is not in my ignore list, with \"!ignore\" I'll not contact this chat again.");
                    else
                    {
                        try
                        {
                            FileHandler.Replace(msg);
                            msg.Chat.SendMessage("Welcome, type \"!help\" to get a list of commands.");
                        }
                        catch (Exception ex)
                        {
                            msg.Chat.SendMessage("Could not unignore: " + ex.ToString());
                        }

                    }
                }
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\skypelog.log", true))
                    writer.WriteLine("[" + DateTime.Now + "] " + "[" + msg.Chat.Name + ", " + msg.Chat.FriendlyName + ", " + msg.Sender.Handle + "]: " + msg.Body);

                //When you get a message from an ignored user
                Writer.WriteIgnored("Get IgnoredChat message: [" + DateTime.Now + "] " + "[" + msg.Chat.Name + ", " + msg.Chat.FriendlyName + ", " + msg.Sender.Handle + "]: ");
                Console.Write(msg.Body + "\n\r");
                return true;
            }
            return false;
        }

        //useless at the moment, I will find a use for it when i finished my socket-file-transferer
        public static void GetContacts()
        {
            skype = new Skype();
            for (int i = 0; i < skype.HardwiredGroups.Count; i++)
            {
                if (skype.HardwiredGroups[i + 1].Type == TGroupType.grpSkypeFriends)
                {
                    for (int j = skype.HardwiredGroups[i + 1].Users.Count; j > 0; j--)
                    {
                        Program._users.Add(skype.HardwiredGroups[i + 1].Users[j].Handle);
                    } break;
                }
            }
        }
    }
}