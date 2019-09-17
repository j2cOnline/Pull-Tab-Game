using System;
using System.Runtime.InteropServices;

namespace BreakTheBankTabs1063
{
    class setLocalTime
    {
        /// <summary>
        /// SYSTEMTIME structure with some useful methods
        /// </summary>
        public struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;

            /// <summary>
            /// Convert form System.DateTime
            /// </summary>
            /// <param name="time"></param>
            public void FromDateTime(DateTime time)
            {
                wYear = (ushort)time.Year;
                wMonth = (ushort)time.Month;
                wDayOfWeek = (ushort)time.DayOfWeek;
                wDay = (ushort)time.Day;
                wHour = (ushort)time.Hour;
                wMinute = (ushort)time.Minute;
                wSecond = (ushort)time.Second;
                wMilliseconds = (ushort)time.Millisecond;
            }

            /// <summary>
            /// Convert to System.DateTime
            /// </summary>
            /// <returns></returns>
            public DateTime ToDateTime()
            {
                return new DateTime(wYear, wMonth, wDay, wHour, wMinute, wSecond, wMilliseconds);
            }
            /// <summary>
            /// STATIC: Convert to System.DateTime
            /// </summary>
            /// <param name="time"></param>
            /// <returns></returns>
            public static DateTime ToDateTime(SYSTEMTIME time)
            {
                return time.ToDateTime();
            }
        }

        [DllImport("Kernel32.dll")]
        public extern static uint SetLocalTime(ref SYSTEMTIME lpSystemTime);
    }
}
