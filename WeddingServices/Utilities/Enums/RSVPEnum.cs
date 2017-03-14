using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WeddingServices.Utilities.Enums
{
    public enum RSVPEnum
    {
        [Description("Assente")]
        ASSENTE = 0,
        [Description("Presente")]
        PRESENTE = 1,
        [Description("Presente solo alla cerimonia")]
        PRESENTE_CERIMONIA = 2,
        [Description("Presente solo al ricevimento")]
        PRESENTE_RICEVIMENTO = 3
    }

    public static class RSVPEnumUtilities
    {
        public static string GetDescription(this RSVPEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
