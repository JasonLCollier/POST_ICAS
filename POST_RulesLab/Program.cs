using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;

namespace POST_RulesLab
{
    class Program
    {

        const String URL = "http://dev.ruleslab.com/rl-engine/icas-risk/ICAS1/1.19";

        const string FILENAME = "D:/Users/jason/Downloads/POST/dummy_input.xml";
        static void Main(string[] args)
        {
            Request request = new Request();

            String inputXml = objectToXMLString(request);
            //String inputXml = getTextFromXMLFile(FILENAME);
            Console.WriteLine(inputXml);

            //String output = postXMLData(URL, inputXml);

            //Console.WriteLine(output);
            Console.ReadLine();
        }

        private static string postXMLData(string destinationUrl, string requestXml)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            request.ContentType = "application/xml";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }
        private static string objectToXMLString(Request request)
        {
            StringWriter stringwriter = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(request.GetType());
            serializer.Serialize(stringwriter, request);
            return stringwriter.ToString();
        }
        private static string getTextFromXMLFile(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            string ret = reader.ReadToEnd();
            reader.Close();
            return ret;
        }

    }
}
