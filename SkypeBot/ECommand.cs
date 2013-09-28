namespace SkypeBot
{
    public enum ECommand
    {
        [StringValue("go offline")]
        GO_OFFLINE,
        [StringValue("hello")]
        HELLO,
        [StringValue("help")]
        HELP,
        [StringValue("date")]
        DATE,
        [StringValue("time")]
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
        [StringValue("ignore me")]
        IGNORE_ME,
        [StringValue("unignore me")]
        UNIGNORE_ME,
    }
    
    public class StringValue : System.Attribute
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
    
    public static class StringEnum : Enum
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
