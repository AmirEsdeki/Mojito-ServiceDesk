using System;
using System.Collections.Generic;
using System.Globalization;

namespace Mojito.ServiceDesk.Application.Common.Extensions
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }

        public static int ToInt(this Enum source) => Convert.ToInt32(source);
        public static long ToLong(this Enum source) => Convert.ToInt64(source);
        public static byte ToByte(this Enum source) => Convert.ToByte(source);


        public static string ToShamsi(this DateTime time)
        {
            PersianCalendar pc = new PersianCalendar();
            return (string.Format("{0}/{1}/{2}", pc.GetYear(time), pc.GetMonth(time), pc.GetDayOfMonth(time)));
        }
    }
}
