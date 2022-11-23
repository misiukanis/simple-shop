using System.ComponentModel;
using System.Reflection;

namespace Shop.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var description = value.GetType()
                                .GetMember(value.ToString())
                                .FirstOrDefault()
                                ?.GetCustomAttribute<DescriptionAttribute>()
                                ?.Description;

            if (description == null) 
            {
                return string.Empty;
            }

            return description;
        }
    }
}
