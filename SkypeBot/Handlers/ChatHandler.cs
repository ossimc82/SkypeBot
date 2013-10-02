﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;

namespace SkypeBot.Handlers
{
    class ChatHandler
    {
        static Skype skype;
        public static void HandleChat(ChatMessage msg)
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
            Writer.WriteGetChat("Get chat: [" + DateTime.Now + "] " + "[" + msg.Chat.Name + ", " + msg.Chat.FriendlyName + ", " + msg.Sender.Handle + "]: ");
            Console.Write(msg.Body + "\n\r");

            //When the bot sends the ressult
            Writer.WriteSuccess("Send Chat: [" + DateTime.Now + "] " + "To [" + msg.Chat.Name + ", " + msg.Chat.FriendlyName + ", " + msg.Sender.Handle + "]: ");
            Console.Write(CommandHandler.ProcessCommand(msg.Body, msg) + "\n\r");
        }
    }
}
