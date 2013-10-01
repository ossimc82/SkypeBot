using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;

namespace SkypeBot
{
    public class FileHandler
    {
        public static void CheckFiles()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot");
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers"))
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers").Close();
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats"))
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats").Close();
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\permitedUsers.users"))
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\permitedUsers.users").Close();
        }

        public static void Write(ChatMessage message, bool IsGroupChat = true)
        {
            if (IsGroupChat)
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats", true))
                    writer.Write(message.Chat.Name + ",");
            else
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers", true))
                    writer.Write(message.Sender.Handle + ",");

            UserListHandler.LoadIgnoreList();
        }

        public static void Replace(ChatMessage message, bool IsGroupChat = true)
        {
            if(IsGroupChat)
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats", File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats").Replace(message.Chat.Name + ",", String.Empty));
            else
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers", File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers").Replace(message.Sender.Handle + ",", String.Empty));

            UserListHandler.LoadIgnoreList();
        }
    }
}
