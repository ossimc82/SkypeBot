using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using SKYPE4COMLib;
using WMPLib;
using SkypeBot.Forms;

namespace SkypeBot.Handlers
{
    public static class ChatCommandHandler
    {
        private static WindowsMediaPlayer mediaPlayer = new WindowsMediaPlayer();
        private static Skype skype = new Skype();
        private static UserController usrc;
        private static string[] permitedUsers;

        public static string ProcessCommand(string str, ChatMessage message)
        {
            usrc = new Forms.UserController();
            string result = String.Empty;

            //Here are the the words that the bot understands.
            #region GO_OFFLINE
            if (str.Equals(StringEnum.GetStringValue(ChatCommand.GO_OFFLINE), StringComparison.InvariantCultureIgnoreCase))
            {
                skype.ChangeUserStatus(TUserStatus.cusInvisible);
                System.Threading.Thread.Sleep(5000);
                skype.ChangeUserStatus(Program.curStatus);
                result = "Back :)";
            }
            #endregion

            #region SAY
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.SAY), StringComparison.InvariantCultureIgnoreCase))
            {
                if (str.StartsWith("!say"))
                    result = "Usage: !say <Your Text>";
            }
            #endregion

            #region DO_I_HAVE_CALLEQUIPMENT
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.DO_I_HAVE_CALLEQUIPMENT), StringComparison.InvariantCultureIgnoreCase))
            {
                if (message.Sender.HasCallEquipment)
                    result = "You have Call Equipment!";
                else
                    result = "You don't have Call Equipment!";
            }
            #endregion

            #region ABOUT_YOU
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.ABOUT_YOU), StringComparison.InvariantCultureIgnoreCase))
            {
                if (String.IsNullOrWhiteSpace(message.Sender.About))
                    result = "Cant parse your about status.";
                else
                    result = "About you: " + message.Sender.About;
            }
            #endregion

            #region HELLO
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.HELLO), StringComparison.InvariantCultureIgnoreCase))
            {
                result = "Hello!";
            }
            #endregion

            #region HELP
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.HELP), StringComparison.InvariantCultureIgnoreCase))
            {
                foreach (Enum i in Enum.GetValues(typeof(ChatCommand)))
                {
                    string output = StringEnum.GetStringValue(i);
                    result = result + output + ", ";
                }
            }
            #endregion

            #region DATE
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.DATE), StringComparison.InvariantCultureIgnoreCase))
            {
                result = "Current Date is: " + DateTime.Now.ToLongDateString();
            }
            #endregion

            #region TIME
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.TIME), StringComparison.InvariantCultureIgnoreCase))
            {
                result = "Current Time is: " + DateTime.Now.ToLongTimeString();
            }
            #endregion

            #region WHO
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.WHO), StringComparison.InvariantCultureIgnoreCase))
            {
                result = "You write with a skype bot, enjoy";
            }
            #endregion

            #region WHO_AM_I
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.WHO_AM_I), StringComparison.InvariantCultureIgnoreCase))
            {
                result = "you are " + message.Sender.Handle + " and your Fullname is " + message.Sender.FullName;
            }
            #endregion

            #region PENIS
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.PENIS), StringComparison.InvariantCultureIgnoreCase))
            {
                result = "vagina";
            }
            #endregion

            #region YOUR_MOTHER
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.YOUR_MOTHER), StringComparison.InvariantCultureIgnoreCase))
            {
                result = "your fish";
            }
            #endregion

            #region HI
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.HI), StringComparison.InvariantCultureIgnoreCase))
            {
                result = "hey :)";
            }
            #endregion

            #region WAKE_HIM_UP
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.WAKE_HIM_UP), StringComparison.InvariantCultureIgnoreCase))
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
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.CONTACTS_AMOUNT), StringComparison.InvariantCultureIgnoreCase))
            {
                result = "You have " + message.Sender.NumberOfAuthBuddies + " contacts.";
            }
            #endregion

            #region IGNORE_ME
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.IGNORE_ME), StringComparison.InvariantCultureIgnoreCase))
            {
                FileHandler.Write(message);
                result = "This chat (" + message.Chat.FriendlyName + "(" + message.Chat.Name + ")) dont get messages from me now, you can enable me with \"!unignore\".";
                usrc.listBox2.Refresh();
            }
            #endregion

            #region UNIGNORE_ME
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.UNIGNORE_ME), StringComparison.InvariantCultureIgnoreCase))
            {
                if (!UserListHandler.Chats.Contains(message.Chat.Name + ","))
                    result = "Sorry but this chat (" + message.Chat.FriendlyName + "(" + message.Chat.Name + ")) are not on my ignore list, with \"!ignore\" I'll not contact you again.";
                else
                    result = "An error ocured while executing the command.";
            }
            #endregion

            #region ABOUT_ME
            else if (str.Equals(StringEnum.GetStringValue(ChatCommand.ABOUT_ME), StringComparison.InvariantCultureIgnoreCase))
            {
                result = "Hello I'm a Skype Bot written by Fabian Fischer you can see my source on github: https://github.com/ossimc82/SkypeBot/";
            }
            #endregion

            else
            {
                result = "Sorry, I do not recognize your command. Type \"!help\" to get a list of all commands. You can disable me with \"!ignore\"";
            }
            return result;
        }
    }
}
