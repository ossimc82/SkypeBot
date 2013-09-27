using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SKYPE4COMLib;
using WMPLib;

namespace SkypeBot
{
    class Program
    {
        static Skype skype;
        static WindowsMediaPlayer mediaPlayer;
        static string[] ignoredUsers;

        static void Main(string[] args)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot");
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers"))
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers").Close();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[" + DateTime.Now + "] Loading...");
            Console.ResetColor();
            skype = new Skype();
            if (skype.CurrentUserStatus != TUserStatus.cusAway)
            {
                skype.ChangeUserStatus(TUserStatus.cusAway);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[" + DateTime.Now + "] Set onlinestatus to: " + skype.CurrentUserStatus.ToString().Replace("cus", String.Empty));
                Console.ResetColor();
            }
            //Loads events
            skype.Attach();
            //Listen 
            skype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);
            //skype.UserStatus += skype_UserStatus;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[" + DateTime.Now + "] Loading complete...");
            Console.ResetColor();

            while (true) { Console.Read(); }
        }

        private static void skype_UserStatus(TUserStatus Status)
        {
            if (Status != TUserStatus.cusAway)
            {
                skype.CurrentUserStatus = TUserStatus.cusAway;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[" + DateTime.Now + "] Set onlinestatus from: " + Status + "to: " + TUserStatus.cusAway.ToString().Replace("cus", String.Empty));
                Console.ResetColor();
                Status = TUserStatus.cusAway;
            }
            else { }
        }

        static void skype_MessageStatus(ChatMessage msg, TChatMessageStatus status)
        {
            ignoredUsers = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers").Split(',');

            if (status == TChatMessageStatus.cmsReceived)
            {
                for (int i = 0; i < ignoredUsers.Length; i++)
                {
                    if (msg.Sender.Handle == ignoredUsers[i])
                    {
                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\skypelog.log", true))
                        {
                            writer.WriteLine("[" + DateTime.Now + "] " + "[" + msg.Sender.Handle + ", " + msg.Sender.FullName + "]: " + msg.Body);
                        }
                        goto skipSendMSG;
                    }
                }

                try
                {
                    //Ignores a user
                    if (ProcessCommand(msg.Body) == "UserIgnore_true")
                    {
                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers", true))
                        {
                            writer.Write(msg.Sender.Handle + ",");
                        }
                        skype.SendMessage(msg.Sender.Handle, "You (" + msg.Sender.Handle + ") dont get message from me now, you can get messages with \"!unignore\".");
                    }
                    //Unignores a user
                    else if (ProcessCommand(msg.Body) == "UserIgnore_false")
                    {
                        string search_text = msg.Sender.Handle;
                        string old;
                        string n = String.Empty;
                        StreamReader reader = File.OpenText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers");
                        while ((old = reader.ReadLine()) != null)
                        {
                            if (!old.Contains(search_text))
                            {
                                skype.SendMessage(msg.Sender.Handle, "Sorry but you are not on my ignore list, with \"!ignore\" I'll not contact you again.");
                            }
                        }
                        reader.Close();
                        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SkypeBot\IgnoredUsers", n);
                    }
                    //Send processed message back to skype chat window
                    else
                    {
                        skype.SendMessage(msg.Sender.Handle, ProcessCommand(msg.Body)
                            .Replace("userHandle", msg.Sender.Handle)
                            .Replace("userFullName", msg.Sender.FullName)
                            .Replace("UserCount", msg.Sender.NumberOfAuthBuddies.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.ToString());
                    Console.ResetColor();
                }

                //When the bot sends the ressult
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Send Chat: [" + DateTime.Now + "] " + "To [" + msg.Sender.Handle + ", " + msg.Sender.FullName + "]: ");
                Console.ResetColor();
                Console.Write(ProcessCommand(msg.Body).Replace("userHandle", msg.Sender.Handle).Replace("userFullName", msg.Sender.FullName).Replace("UserCount", msg.Sender.NumberOfAuthBuddies.ToString()) + "\n\r");

            skipSendMSG:
                //When you get a message
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Get chat: [" + DateTime.Now + "] " + "[" + msg.Sender.Handle + ", " + msg.Sender.FullName + "]: ");
                Console.ResetColor();
                Console.Write(msg.Body + "\n\r");
            }
        }

        static string ProcessCommand(string str)
        {
            string result;
            //Here are the the words that the bot understands.
            switch (str)
            {
                case "go offline!":
                    result = "go_offline_true";
                    break;
                case "hello":
                    result = "Hello!";
                    break;
                case "!help":
                    result = "go offline!, hello, !help, date, time, who, who am i?, penis, your mother, hi, wake him up!!!, my contacts, !ignore, !unignore";
                    break;
                case "date":
                    result = "Current Date is: " + DateTime.Now.ToLongDateString();
                    break;
                case "time":
                    result = "Current Time is: " + DateTime.Now.ToLongTimeString();
                    break;
                case "who":
                    result = "You write with a skype bot, enjoy";
                    break;
                case "who am i?":
                    result = "you are \"userHandle\" and your Fullname is \"userFullName\"";
                    break;
                case "penis":
                    result = "vagina";
                    break;
                case "your mother":
                    result = "your fish";
                    break;
                case "hi":
                    result = "hey :)";
                    break;
                case "wake him up!!!":
                    result = "wake_up = true";
                    break;
                case "my contacts":
                    result = "You have UserCount contacts.";
                    break;
                case "!ignore":
                    result = "UserIgnore_true";
                    break;
                case "!unignore":
                    result = "UserIgnore_false";
                    break;
                default:
                    result = "Sorry, I do not recognize your command. Type \"!help\" to get a list of all commands. You can disable me with \"!ignore\"";
                    break;
            }

            if (result == "wake_up = true")
            {
                mediaPlayer = new WindowsMediaPlayer();
                mediaPlayer.URL = "http://countersossi.co.funpic.de/rest/Linkin%20Park%20-Leave%20out%20all%20the%20rest%20-%20Lyrics.mp3";
                mediaPlayer.controls.play();
                return "Let us wake up this asshole, I play music for him :)";
            }
            else if (result == "go_offline_true")
            {
                //you will go back online after 5 sec
                skype.ChangeUserStatus(TUserStatus.cusOffline);
                System.Threading.Thread.Sleep(5000);
                skype.ChangeUserStatus(TUserStatus.cusAway);
                return "";
            }
            else
            {
                return result;
            }
        }
    }

    //public static class StringAdd
    //{
    //    static Skype skype;
    //    public static string ExecuteVoid(this string skypename, string )
    //    {
    //        return null;
    //    }
    //}
}
