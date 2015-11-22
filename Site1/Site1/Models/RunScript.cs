using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Site1.Models
{
    /* EF Script oynatıcı 
     * EF Production dilinde yazılmış scriptleri oynatacaktır. 
     * Bu senaryolar tamamen kod yazılmadan oynatmaya yarayacak ve veritabanına müdahale edebilecektir.
     * Scriptin amacı son kullanıcıya teslim edildikten sonra kolayca eklemeler yapılmasını sağlayacaktır. 
     * Yapılacak olan eklemeler tam erişime sahip olmayacağından büyük sistemleri içermeyecektir. 
     * Fatih KÖSE 22.11.2015 */

    public class RunScript
    {
        public string Layout { get; set; }
        private String Script { get; set; }
        public HtmlString Content { get{ return new HtmlString(ContentText); } }
        private String ContentText = string.Empty;

        public RunScript(string Script)
        {
            this.Script = HttpContext.Current.Server.MapPath("~/settings/EFScipts/" + Script);
        }

        public void Run()
        {
            try {
                // Script dosyasının tamamını okuyalım.
                StreamReader objReader = new StreamReader(this.Script);
                string sLine = string.Empty;
                string data = string.Empty;
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        data += sLine + Environment.NewLine;
                }
                objReader.Close();

                // Scriptleri uygulayalım
                foreach (string line in data.Split(new string[] { "<command>" }, StringSplitOptions.None))
                {
                    bool isScript = false;
                    line.Replace("</command>", "");

                    string[] Data = line.Split(new string[] { "<template>", "</template>" }, StringSplitOptions.None);
                    if (Data.Length >= 2)
                    {
                        Layout = Data[1].Trim(); // Template oku
                        isScript = true;
                    }

                    Data = line.Split(new string[] { "<database=update>", "</database>" }, StringSplitOptions.None);
                    if (Data.Length >= 2) // Database güncelleme
                    {
                        Data = Data[1].Split(new string[] { "<table>", "</table>", "<column>", "<datatype>", "</datatype>", "<value>", "</value>", "</column>" }, StringSplitOptions.None);

                        Tuple<string, string, string> Values = new Tuple<string, string, string>(Data[1], Data[5], Data[7]);

                        // Gelen veriye gödere işlem yapılacak

                        isScript = true;
                    }


                    if (isScript)
                        continue;
                    ContentText = line;
                }
            }
            catch
            {
                Layout = null;
                ContentText = "<h3><b>EF Engine : </b> Üzgünüm, script çalıştırılamadı. Daha sonra tekrar deneyiniz.</h3>";
            }
        }
    }
}