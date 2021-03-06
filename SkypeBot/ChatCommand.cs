using System;
using System.Reflection;

namespace SkypeBot
{
    public enum ChatCommand
    {
        [StringValue("go offline!")]
        GO_OFFLINE,
        [StringValue("hello")]
        HELLO,
        [StringValue("!help")]
        HELP,
        [StringValue("whats the current date")]
        DATE,
        [StringValue("whats the current time")]
        TIME,
        [StringValue("who")]
        WHO,
        [StringValue("who am i?")]
        WHO_AM_I,
        [StringValue("penis")]
        PENIS,
        [StringValue("your mother")]
        YOUR_MOTHER,
        [StringValue("hi")]
        HI,
        [StringValue("wake him up!")]
        WAKE_HIM_UP,
        [StringValue("contacts amount")]
        CONTACTS_AMOUNT,
        [StringValue("!ignore")]
        IGNORE_ME,
        [StringValue("!unignore")]
        UNIGNORE_ME,
        [StringValue("!about")]
        ABOUT_ME,
        [StringValue("!say")]
        SAY,
        [StringValue("!callequipment")]
        DO_I_HAVE_CALLEQUIPMENT,
        [StringValue("!aboutMe")]
        ABOUT_YOU,
    }
    
    public class StringValue : Attribute
    {
        private string _value;
        
        public StringValue(string value)
        {
            this._value = value;
        }
        
        public string Value
        {
            get { return _value; }
        }
    }
    
    public static class StringEnum
    {
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
    
            return output;
        }
    }
}
