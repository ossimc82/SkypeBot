using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeBot.Handlers
{
    class ConsoleCommandHandler
    {
        public static string ProcessCommand(string command)
        {
            string result = null;

            try
            {
                #region HELP
                if (command.Equals(StringEnum.GetStringValue(ConsoleCommand.HELP), StringComparison.InvariantCultureIgnoreCase))
                {
                    string output = null;
                    foreach (Enum i in Enum.GetValues(typeof(ConsoleCommand)))
                    {
                        output = StringEnum.GetStringValue(i);
                        result = result + output + ",\n";
                    }
                }
                #endregion

                #region CLEAR
                else if (command.Equals(StringEnum.GetStringValue(ConsoleCommand.CLEAR), StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.Clear();
                }
                #endregion

                #region SHOW_FORM
                else if (command.Equals(StringEnum.GetStringValue(ConsoleCommand.SHOW_FORM), StringComparison.InvariantCultureIgnoreCase))
                {
                    new Forms.UserController().ShowDialog();
                }
                #endregion

                else
                {
                    Writer.WriteErrorln("Unknown command, type \"help\" to get a list of all commands");
                    return null;
                }
                return result;
            }
            catch { return null; }
        }
    }
}
