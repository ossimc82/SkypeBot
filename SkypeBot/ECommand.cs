namespace SkypeBot
{
    public enum ECommand
    {
        GO_OFFLINE = "go offline",
        HELLO = "hello",
        HELP = "help",
        DATE = "date",
        TIME = "time",
        WHO = "who",
        WHO_AM_I = "who am i?",
        PENIS = "penis",
        YOUR_MOTHER = "your mother",
        HI = "hi",
        WAKE_HIM_UP = "wake him up!",
        CONTATCS_AMOUNT = "contacts amount",
        IGNORE_ME = "ignore me",
        UNIGNORE_ME = "unignore me"
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
