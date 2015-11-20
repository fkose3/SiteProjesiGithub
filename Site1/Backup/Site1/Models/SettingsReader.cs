using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Site1.Models
{
    public static class SettingsReader
    {
        public static string Read(string Section, string key, string lang = "tr")
        {
            string Default = String.Empty;
            StringBuilder StrBuild = new StringBuilder(256);
            string path = HttpContext.Current.Server.MapPath("~/settings/setting.ini");
            Debug.Write(path);
            GetPrivateProfileString(Section, key, Default, StrBuild, 255, path);
            byte[] bytes = Encoding.Default.GetBytes(StrBuild.ToString());
            return Encoding.UTF8.GetString(bytes);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        public static long Write(string section, string key, string value)
        {
            string path = HttpContext.Current.Server.MapPath("~/settings/setting.ini");
            return WritePrivateProfileString(section, key, value, path);
        }
    }
}