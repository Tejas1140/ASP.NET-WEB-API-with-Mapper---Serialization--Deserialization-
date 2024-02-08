using System.Text.RegularExpressions;

namespace WebApp2.ApplicationLayer.Utility
{
    public class AppUtility
    {
        // Regular expression to check if string is a Indian mobile number
        private const string _regexExpForMobile = @"^[6789]\d{9}$";
        public static bool CheckMobileNumber(string mobileNumber)
        {
            try
            {
                return Regex.IsMatch(mobileNumber, _regexExpForMobile);
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
