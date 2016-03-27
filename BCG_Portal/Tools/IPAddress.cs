using System;
using System.Web;
using System.Text.RegularExpressions;

namespace Tools
{
    public class IPAddress
    {
        public IPAddress()
        {
            // 
            // TODO: Add constructor logic here
            //
        }

        static public String GetClientIPAddress(HttpContext _context)
        {
            String ipAddress = (_context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && _context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "" && _context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "UNKNOWN" && _context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "unknown") ? _context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : _context.Request.ServerVariables["REMOTE_ADDR"];

            if (ipAddress.IndexOf(',') != -1)
            {
                String[] ipAddresses = ipAddress.Split(',');
                foreach (String IPAddress in ipAddresses)
                {
                    if (CheckIPAddress(IPAddress))
                        return IPAddress.Trim();
                }
            }

            if (CheckIPAddress(ipAddress))
            {
                return ipAddress.Trim();
            }
            else
            {
                return String.Empty;
            }
               
        }


        static protected Boolean CheckIPAddress(String _ipAddress)
        {
            _ipAddress = _ipAddress.Trim();
            Match matchIP = Regex.Match(_ipAddress, @"^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$");

            return matchIP.Success;
        }
    }
}
