using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeBot
{
    public enum ConsoleCommand
    {
        [StringValue("help")]
        HELP,
        [StringValue("clear")]
        CLEAR,
        [StringValue("visual")]
        SHOW_FORM
    }
}
