﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;

namespace SkypeBot.Handlers
{
    public class FileHandler
    {
        public static void CheckFiles()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot");
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats"))
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats").Close();
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\permitedUsers.users"))
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\permitedUsers.users").Close();
        }

        public static void Write(ChatMessage message)
        {
            TextWriter tw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats", true);
            tw.Write(message.Chat.Name + ",");
            tw.Close();
            UserListHandler.LoadIgnoreList();
        }

        public static void Replace(ChatMessage message)
        {
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats", File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats").Replace(message.Chat.Name + ",", String.Empty));
            UserListHandler.LoadIgnoreList();
        }

        public static void Replace(string chatID)
        {
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats", File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats").Replace(chatID + ",", String.Empty));
            UserListHandler.LoadIgnoreList();
        }
    }
}
