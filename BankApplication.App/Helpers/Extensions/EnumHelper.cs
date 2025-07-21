using System;
using System.ComponentModel;
using System.Reflection;

public static class EnumHelper
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        if (field != null &&
            Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
        {
            return attribute.Description;
        }

        return value.ToString();
    }
}