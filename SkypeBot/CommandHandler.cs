﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using SKYPE4COMLib;
using WMPLib;

namespace SkypeBot
{
    public static class CommandHandler
    {
        private static WindowsMediaPlayer mediaPlayer = new WindowsMediaPlayer();
        private static Skype skype = new Skype();
        private static string[] permitedUsers;

        public static string ProcessCommand(string str, ChatMessage message)
        {
            string result = String.Empty;

            //Here are the the words that the bot understands.
            #region GO_OFFLINE
            if (str.Equals(StringEnum.GetStringValue(ECommand.GO_OFFLINE)))
            {
                skype.ChangeUserStatus(TUserStatus.cusInvisible);
                System.Threading.Thread.Sleep(5000);
                skype.ChangeUserStatus(Program.curStatus);
                result = "Back :)";
            }
            #endregion

            #region SAY
            else if (str.Equals(StringEnum.GetStringValue(ECommand.SAY)))
            {
                if (str.StartsWith("!say"))
                    result = "Usage: !say <Your Text>";
            }
            #endregion

            #region DO_I_HAVE_CALLEQUIPMENT
            else if (str.Equals(StringEnum.GetStringValue(ECommand.DO_I_HAVE_CALLEQUIPMENT)))
            {
                if (message.Sender.HasCallEquipment)
                    result = "You have Call Equipment!";
                else
                    result = "You don't have Call Equipment!";
            }
            #endregion

            #region ABOUT_YOU
            else if (str.Equals(StringEnum.GetStringValue(ECommand.ABOUT_YOU)))
            {
                result = "About you: " + message.Sender.About;
            }
            #endregion

            #region HELLO
            else if (str.Equals(StringEnum.GetStringValue(ECommand.HELLO)))
            {
                result = "Hello!";
            }
            #endregion

            #region HELP
            else if (str.Equals(StringEnum.GetStringValue(ECommand.HELP)))
            {
                foreach (var i in Enum.GetValues(typeof(ECommand)))
                {
                    string output = null;
                    Type type = i.GetType();
                    FieldInfo fi = type.GetField(i.ToString());
                    StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
                    if (attrs.Length > 0)
                    {
                        output = attrs[0].Value;
                    }
                    result = result + output + ", ";
                }
            }
            #endregion

            #region DATE
            else if (str.Equals(StringEnum.GetStringValue(ECommand.DATE)))
            {
                result = "Current Date is: " + DateTime.Now.ToLongDateString();
            }
            #endregion

            #region TIME
            else if (str.Equals(StringEnum.GetStringValue(ECommand.TIME)))
            {
                result = "Current Time is: " + DateTime.Now.ToLongTimeString();
            }
            #endregion

            #region WHO
            else if (str.Equals(StringEnum.GetStringValue(ECommand.WHO)))
            {
                result = "You write with a skype bot, enjoy";
            }
            #endregion

            #region WHO_AM_I
            else if (str.Equals(StringEnum.GetStringValue(ECommand.WHO_AM_I)))
            {
                result = "you are " + message.Sender.Handle + " and your Fullname is " + message.Sender.FullName;
            }
            #endregion

            #region PENIS
            else if (str.Equals(StringEnum.GetStringValue(ECommand.PENIS)))
            {
                result = "vagina";
            }
            #endregion

            #region YOUR_MOTHER
            else if (str.Equals(StringEnum.GetStringValue(ECommand.YOUR_MOTHER)))
            {
                result = "your fish";
            }
            #endregion

            #region HI
            else if (str.Equals(StringEnum.GetStringValue(ECommand.HI)))
            {
                result = "hey :)";
            }
            #endregion

            #region WAKE_HIM_UP
            else if (str.Equals(StringEnum.GetStringValue(ECommand.WAKE_HIM_UP)))
            {
                // %appdata%\SkypeBot
                permitedUsers = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\permitedUsers.users").Split(',');
                
                for (int i = 0; i < permitedUsers.Length; i++)
                {
                    if (message.Sender.Handle.Equals(permitedUsers[i]))
                    {
                        mediaPlayer = new WindowsMediaPlayer();
                        mediaPlayer.URL = "http://countersossi.co.funpic.de/rest/Linkin%20Park%20-Leave%20out%20all%20the%20rest%20-%20Lyrics.mp3";
                        mediaPlayer.controls.play();
                        result = "Let us wake up this asshole, I play music for him :)";
                        break;
                    }
                    else
                        result = "MOTHERFUCKER (angry), You do not have permission to use this command!!!!!!!!";
                }
            }
            #endregion

            #region CONTACTS_AMOUNT
            else if (str.Equals(StringEnum.GetStringValue(ECommand.CONTACTS_AMOUNT)))
            {
                result = "You have " + message.Sender.NumberOfAuthBuddies + " contacts.";
            }
            #endregion

            #region IGNORE_CHAT
            else if (str.Equals(StringEnum.GetStringValue(ECommand.IGNORE_CHAT)))
            {
                FileHandler.Write(message);
                result = "This conversation (" + message.Chat.Name + ") dont get messages from me now, you can enable me with \"!unignore_chat\".";
            }
            #endregion

            #region UNIGNORE_CHAT
            else if (str.Equals(StringEnum.GetStringValue(ECommand.UNIGNORE_CHAT)))
            {
                string name = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers");
                if (!name.Contains(message.Chat.Name + ","))
                    result = "Sorry but this conversation (" + message.Chat.Name + ") is not in my ignore list, with \"!ignore_chat\" I'll not contact this group again.";
                else
                    result = "An error ocured while executing the command.";
            }
            #endregion

            #region IGNORE_ME
            else if (str.Equals(StringEnum.GetStringValue(ECommand.IGNORE_ME)))
            {
                FileHandler.Write(message, false);
                result = "You (" + message.Sender.Handle + ") dont get messages from me now, you can enable me with \"!unignore\".";
            }
            #endregion

            #region UNIGNORE_ME
            else if (str.Equals(StringEnum.GetStringValue(ECommand.UNIGNORE_ME)))
            {
                string name = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers");
                if (!name.Contains(message.Sender.Handle + ","))
                    result = "Sorry but you (" + message.Sender.Handle + ") are not on my ignore list, with \"!ignore\" I'll not contact you again.";
                else
                    result = "An error ocured while executing the command.";
            }
            #endregion

            #region ABOUT_ME
            else if (str.Equals(StringEnum.GetStringValue(ECommand.ABOUT_ME)))
            {
                result = "Hello I'm a Skype Bot written by Fabian Fischer you can see my source on github: https://github.com/ossimc82/SkypeBot/";
            }
            #endregion

            else
            {
                if (message.Chat.Members.Count > 2)
                    result = "Sorry, I do not recognize your command. Type \"!help\" to get a list of all commands. You can disable me with \"!ignore\"";
                else
                    result = "Sorry, I do not recognize your command. Type \"!help\" to get a list of all commands. You can disable me in this chat with \"!ignore_chat\"";
            }
            return result;
        }
    }
}