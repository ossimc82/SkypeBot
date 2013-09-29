using System;
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
    public static class CommandProcessor
    {
        private static WindowsMediaPlayer mediaPlayer = new WindowsMediaPlayer();
        private static Skype skype = new Skype();
        private static string[] permitedUsers;

        public static string ProcessCommand(string str, ChatMessage message)
        {
            string result = String.Empty;

            //Here are the the words that the bot understands.
            if (str.Equals(StringEnum.GetStringValue(ECommand.GO_OFFLINE)))
            {
                skype.ChangeUserStatus(TUserStatus.cusInvisible);
                System.Threading.Thread.Sleep(5000);
                skype.ChangeUserStatus(Program.curStatus);
                result = "Back :)";
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.SAY)))
            {
                if (str.StartsWith("!say"))

                result = "Usage: !say <Your Text>";
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.DO_I_HAVE_CALLEQUIPMENT)))
            {
                if (message.Sender.HasCallEquipment)
                    result = "You have Call Equipment!";
                else
                    result = "You don't have Call Equipment!";
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.ABOUT_ME)))
            {
                result = "About you: " + message.Sender.About;
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.HELLO)))
            {
                result = "Hello!";
            }

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

            else if (str.Equals(StringEnum.GetStringValue(ECommand.DATE)))
            {
                result = "Current Date is: " + DateTime.Now.ToLongDateString();
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.TIME)))
            {
                result = "Current Time is: " + DateTime.Now.ToLongTimeString();
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.WHO)))
            {
                result = "You write with a skype bot, enjoy";
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.WHO_AM_I)))
            {
                result = "you are " + message.Sender.Handle + " and your Fullname is " + message.Sender.FullName;
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.PENIS)))
            {
                result = "vagina";
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.YOUR_MOTHER)))
            {
                result = "your fish";
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.HI)))
            {
                result = "hey :)";
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.WAKE_HIM_UP)))
            {
                // %appdata%\SkypeBot
                permitedUsers = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats").Split(',');
                
                for (int i = 0; i < permitedUsers.Length; i++)
                {
                    if (message.Sender.Handle == permitedUsers[i])
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

            else if (str.Equals(StringEnum.GetStringValue(ECommand.CONTACTS_AMOUNT)))
            {
                result = "You have " + message.Sender.NumberOfAuthBuddies + " contacts.";
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.IGNORE_CHAT)))
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredChats", true))
                {
                    writer.Write(message.Chat.Name + ",");
                }
                result = "This conversation (" + message.Chat.Name + ") dont get messages from me now, you can enable me with \"!unignore_chat\".";
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.UNIGNORE_CHAT)))
            {
                string name = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers");
                if (!name.Contains(message.Chat.Name + ","))
                    result = "Sorry but this conversation (" + message.Chat.Name + ") is not in my ignore list, with \"!ignore_chat\" I'll not contact this group again.";
                else
                    result = "An error ocured while executing the command.";
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.IGNORE_ME)))
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers", true))
                {
                    writer.Write(message.Sender.Handle + ",");
                }
                result = "You (" + message.Sender.Handle + ") dont get messages from me now, you can enable me with \"!unignore\".";
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.UNIGNORE_ME)))
            {
                string name = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers");
                if (!name.Contains(message.Sender.Handle + ","))
                    result = "Sorry but you (" + message.Sender.Handle + ") are not on my ignore list, with \"!ignore\" I'll not contact you again.";
                else
                    result = "An error ocured while executing the command.";
            }

            else if (str.Equals(StringEnum.GetStringValue(ECommand.ABOUT_ME)))
            {
                result = "Hello I'm a Skype Bot written by Fabian Fischer you can see my source on github: https://github.com/ossimc82/SkypeBot/";
            }

            else
            {
                if (message.Type == TChatMessageType.cmeSaid)
                {
                    result = "Sorry, I do not recognize your command. Type \"!help\" to get a list of all commands. You can disable me in this chat with \"!ignore_chat\"";
                }
                else
                    result = "Sorry, I do not recognize your command. Type \"!help\" to get a list of all commands. You can disable me with \"!ignore\"";
            }
            return result;
        }
    }
}
