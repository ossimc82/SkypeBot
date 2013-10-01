using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;

namespace SkypeBot
{
    class ChatHandler
    {
        static Skype skype;
        public static void HandleGroupChat(ChatMessage msg)
        {
            try
            {
                if (msg.Body.StartsWith("!say "))
                {
                    msg.Chat.SendMessage(msg.Body.Replace("!say ", String.Empty));
                }
                else
                {
                    //Send processed message back to skype chat window
                    msg.Chat.SendMessage(CommandHandler.ProcessCommand(msg.Body, msg));
                }
            }
            catch (Exception ex)
            {
                Writer.WriteErrorln(ex.ToString());
            }
            //When you get a message
            Writer.WriteGetChat("Get chat: [" + DateTime.Now + "] " + "[" + msg.Chat.Name + ", " + msg.Chat.FriendlyName + "]: ");
            Console.Write(msg.Body + "\n\r");

            //When the bot sends the ressult
            Writer.WriteSuccess("Send Chat: [" + DateTime.Now + "] " + "To [" + msg.Chat.Name + ", " + msg.Chat.FriendlyName + "]: ");
            Console.Write(CommandHandler.ProcessCommand(msg.Body, msg) + "\n\r");
        }

        public static void HandleUserChat(ChatMessage msg)
        {
            skype = new Skype();

            try
            {
                if (msg.Body.StartsWith("!say "))
                {
                    skype.SendMessage(msg.Sender.Handle, msg.Body.Replace("!say ", String.Empty));
                }
                else
                {
                    //Send processed message back to skype chat window
                    skype.SendMessage(msg.Sender.Handle, CommandHandler.ProcessCommand(msg.Body, msg));
                }
            }
            catch (Exception ex)
            {
                Writer.WriteErrorln(ex.ToString());
            }
            //When you get a message
            Writer.WriteGetChat("Get chat: [" + DateTime.Now + "] " + "[" + msg.Sender.Handle + ", " + msg.Sender.FullName + "]: ");
            Console.Write(msg.Body + "\n\r");

            //When the bot sends the ressult
            Writer.WriteSuccess("Send Chat: [" + DateTime.Now + "] " + "To [" + msg.Sender.Handle + ", " + msg.Sender.FullName + "]: ");
            Console.Write(CommandHandler.ProcessCommand(msg.Body, msg) + "\n\r");
        }
    }
}
