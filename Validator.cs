using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class Validator
    {
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public static bool validatePhone(string phone)
        {
            return Regex.IsMatch(phone, @"[0-9]+");
        }
        public static bool validateDate(string str)
        {
            bool ret = false;
            bool primaryCheck = Regex.IsMatch(str, @"[0-9]{4}\-[0-9]{2}\-[0-9]{2}");
            if(primaryCheck)
            {
                string[] date_parts = str.Split('-');
                ret = Int32.Parse(date_parts[1]) < 13 && Int32.Parse(date_parts[2]) < 32;
            }
            return ret;
        }
        public static bool validateChar(string str)
        {
            return Regex.IsMatch(str, @"[a-zA-Z]+");
        }
        public static bool validateMoney(string str)
        {
            return Regex.IsMatch(str, @"[0-9\.]+");
        }

    }
}
