using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;
using System.IO;

namespace SkypeBot
{
    public static class UserList
    {
        private static List<string> Users;
        private static List<string> Chats;
        private static string[] ignoredUsers;
        private static string[] ignoredChats;
        static Skype skype;

        public static void LoadIgnoreList()
        {
            Users = new List<string>();
            Chats = new List<string>();
            ignoredUsers = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers").Split(',');
            ignoredChats = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats").Split(',');
            
            for (int i = 0; i < ignoredUsers.Length; i++)
            {
                Users.Add(ignoredUsers[i]);
            }

            for (int i = 0; i < ignoredChats.Length; i++)
            {
                Chats.Add(ignoredChats[i]);
            }
        }

        public static bool IsUserIgnored(ChatMessage msg)
        {
            skype = new Skype();

            if (Users.Contains(msg.Sender.Handle))
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

                //When you get a message from an ignored chat
                Writer.WriteIgnored("Get IgnoredUser chat: [" + DateTime.Now + "] " + "[" + msg.Sender.Handle + ", " + msg.Sender.FullName + "]: ");
                Console.Write(msg.Body + "\n\r");
                return true;
            }
            return false;
        }

        public static bool IsChatIgnored(ChatMessage msg)
        {
            if(Chats.Contains(msg.Chat.Name))
            {
                if (msg.Body == "!unignore_chat")
                {
                    if (!Chats.Contains(msg.Chat.Name))
                        msg.Chat.SendMessage("Sorry but this group is not in my ignore list, with \"!ignore_chat\" I'll not contact this group again.");
                    else
                        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats", File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats").Replace(msg.Chat.Name + ",", String.Empty));
                }
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\skypelog_chats.log", true))
                {
                    writer.WriteLine("[" + DateTime.Now + "] " + "[" + msg.Chat.Name + ", " + msg.Chat.FriendlyName + "]: " + msg.Body);
                }

                //When you get a message from an ignored user
                Writer.WriteIgnored("Get IgnoredChat message: [" + DateTime.Now + "] " + "[" + msg.Chat.Name + ", " + msg.Chat.FriendlyName + "]: ");
                Console.Write(msg.Body + "\n\r");
                return true;
            }
            return false;
        }

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
