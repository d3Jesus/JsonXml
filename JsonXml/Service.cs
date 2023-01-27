using Newtonsoft.Json;
using System.Xml;

namespace JsonXml
{
    public static class Service
    {
        public static void Converter(IWebHostEnvironment web, IFormFile file)
        {
            var url = Path.Combine(web.WebRootPath, file.FileName);

            SaveFile(file, url);

            string json = File.ReadAllText(url);

            WriteToXML(web.WebRootPath, json, file.FileName);
        }

        private static void WriteToXML(string rootPath, string json, string fileName)
        {
            fileName = fileName.Split('.')[0];

            var url = Path.Combine(rootPath, string.Concat(fileName, ".xml"));
            XmlDocument document = JsonConvert.DeserializeXmlNode(json)!;
            document.Save(url);
        }

        private static void SaveFile(IFormFile file, string url)
        {
            using (var memStream = new MemoryStream())
            {
                file.CopyTo(memStream);
                
                using (FileStream fileStream = new FileStream(url, FileMode.Create))
                {
                    memStream.WriteTo(fileStream);
                }
            }
        }
    }
}
