using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio.Utils
{
    public static class StringExtensions
    {
        public static T ToEnum<T>(this string str, bool ignoreCase = true)
        {
            return (T)Enum.Parse(typeof(T), str, ignoreCase);
        }
    }
}