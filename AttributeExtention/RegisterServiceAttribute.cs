using System;

namespace AttributeExtentions
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RegisterServiceAttribute : Attribute
    {
        public RegisterServiceAttribute()
        {

        }
    }
}
