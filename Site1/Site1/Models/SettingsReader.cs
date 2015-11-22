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
            return RunScript(Encoding.UTF8.GetString(bytes));
        }

        private static string RunScript(string NonScript)
        {
            // Link kontrolü
            string[] Data = NonScript.Split(new string[] { "<link>", "</link>" }, StringSplitOptions.None);

            String ScriptResult = NonScript;

            if (Data.Length >= 2)
                ScriptResult = ToLink(Data);

            Data = ScriptResult.Split(new string[] { "<sitename>","<phone>","<mail>" }, StringSplitOptions.None);
            if (Data.Length >= 2)
                ScriptResult = ApeendOption(ScriptResult);

            Data = ScriptResult.Split(new string[] { "<thisyear>" }, StringSplitOptions.None);

            if (Data.Length >= 2)
                ScriptResult = ToDateTime(Data,1);

            return ScriptResult;
        }

        private static string ApeendOption(string scriptResult)
        {
            return scriptResult;
        }

        // Okunan veriyi tarih haline getir
        private static string ToDateTime(string[] data, int type)
        {
            String result = data[0] != null && data[0] != String.Empty ? data[0] : String.Empty;
            switch(type)
            {
                case 1: // Yıl
                    result += DateTime.Now.Year.ToString();
                    break;
                case 2: // Ay 
                    result += DateTime.Now.Month.ToString();
                    break;
                case 3: // Gün
                    result += DateTime.Now.Month.ToString();
                    break;
                case 4: // Saat
                    result += DateTime.Now.Month.ToString();
                    break;
                case 5: // Dakika
                    result += DateTime.Now.Month.ToString();
                    break;
            }

            result += data[1] != null && data[1] != String.Empty ? data[1] : String.Empty;

            return result;
        }

        // Okunan veriyi link haline getir
        private static string ToLink(string[] data)
        {
            try
            {
                String result = data[0] != null && data[0] != string.Empty ? data[0] + "<a " : "<a ";
                // Gelen link verisi içinden bilgileri al
                String[] Url = data[1].Split(new String[] { "<url>", "</url>" },StringSplitOptions.None);
                String[] Class = data[1].Split(new String[] { "<class>", "</class>" }, StringSplitOptions.None);
                String[] Style = data[1].Split(new String[] { "<style>", "</style>" }, StringSplitOptions.None);
                String[] Title = data[1].Split(new String[] { "<title>", "</title>" }, StringSplitOptions.None);
                // Bilgilere göre link oluştur.
                if(Url.Length >= 2)
                    result += String.Format("href=\"{0}\" ", Url[1]);
                if (Class.Length >= 2)
                    result += String.Format("Class=\"{0}\" ", Class[1]);
                if (Style.Length >= 2)
                    result += String.Format("Style=\"{0}\" ", Style[1]);

                result += ">";

                if (Title.Length >= 2)
                    result += String.Format("{0}", Title[1]);

                result += data[2] != null && data[2] != string.Empty ? "</a>" + data[2] : "</a>" ;

                return result;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Hata : {0} | {1}" , ex.Message, DateTime.Now.ToShortDateString());
                return String.Empty;
            }
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