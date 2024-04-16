using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerStatusChecklist
{
    class StatusColorSelection
    {
        private static string lightGreen = "#FF90EE90";
        private static string lightOrange = "#FFFFC443";
        private static string lightRed = "#FFFF6767";
        private static string lightGray = "#FFAFAFAF";

        public static string LightGreen { get { return lightGreen; } }
        public static string LightOrange { get { return lightOrange; } }
        public static string LightRed { get { return lightRed; } }
        public static string LightGray { get { return lightGray; } }
    }
}