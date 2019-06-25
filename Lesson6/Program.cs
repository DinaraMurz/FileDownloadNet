using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;

namespace Lesson6
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri url = new Uri("http://www.google.ru");
            //Uri url2 = new Uri("ftp://www.google.ru"); 

            //HttpWebRequest httpWeb = (HttpWebRequest)HttpWebRequest.Create("http://mail.ru");
            HttpWebRequest httpWeb = WebRequest.CreateHttp("http://mail.ru");

            httpWeb.Method = WebRequestMethods.Http.Head;
            //httpWeb.Headers.Add("user-agent", "Mozilla/5.0");
            httpWeb.UserAgent = "Mozilla/5.0";


            foreach (var a in httpWeb.Headers)
            {
                Console.WriteLine(a.ToString());
            }

            //httpWeb.Headers["user-agent"] = "Mozilla/5.0"; //приходила бы полноценная картинка/экран 
                                                           //в любом лучаи(не мобильная на телефон)

            // получить ответ сервера
            HttpWebResponse httpResp = (HttpWebResponse)httpWeb.GetResponse();

            for (int i = 0; i < httpWeb.Headers.Count; i++)
            {
                Console.Write(
                    httpWeb.Headers.Keys[i] + ": ");
                foreach (var a in httpWeb.Headers.GetValues(i))
                {
                    Console.WriteLine(a.ToString());
                }
                //httpResp.Headers.GetValues(i));
            }
            Console.WriteLine("-------------------------");

            for(int i = 0; i < httpResp.Headers.Count; i++)
            {
                Console.Write(
                    httpResp.Headers.Keys[i] + ": ");
                foreach (var a in httpResp.Headers.GetValues(i))
                {
                    Console.WriteLine(a.ToString());
                }
                //httpResp.Headers.GetValues(i));
            }
            //foreach (var a in httpWeb.Headers)
            //{
            //    Console.WriteLine(a.ToString());
            //}

            // good try
            //http
            //httpWeb.ClientCertificates = new System.Security.Cryptography.X509Certificates.X509CertificateCollection();
            //httpWeb.UseDefaultCredentials = true;
            //httpWeb

            // чтение файла с http-ресурса
            HttpWebRequest webFile = WebRequest.CreateHttp("https://i.gifer.com/Czhl.gif");
            webFile.Method = WebRequestMethods.Http.Get;
            HttpWebResponse webFileResponse = (HttpWebResponse)webFile.GetResponse();


            Stream pngStream = webFileResponse.GetResponseStream();

            using (BinaryReader stream = new BinaryReader(pngStream))
            {
                //1 stream ==> fileStream
                FileStream fileStream = new FileStream("NewFile.gif", FileMode.Create);
                //string strbuf = stream.ReadToEnd();
                //fileStream.Write(Encoding.Default.GetBytes(strbuf), 0, strbuf.Length);

                fileStream.Write(stream.ReadBytes((int)webFileResponse.ContentLength), 0, (int)webFileResponse.ContentLength);


                // stream ==> streamWrite
                //StreamWriter streamWriter = new StreamWriter(fileStream);
                //streamWriter.Write(stream.ReadToEnd());

                Console.WriteLine($"В файл NewFile.jpg записано {webFileResponse.ContentLength} байт");
            }

            Console.ReadLine();
        }
    }
}
