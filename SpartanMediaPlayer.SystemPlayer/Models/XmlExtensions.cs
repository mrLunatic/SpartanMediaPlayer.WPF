using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpartanMediaPlayer.Models
{
    public static class XmlExtensions
    {
        public static void SaveXml(this PlayList playList, string fileName)
        {
            var serializer = new XmlSerializer(typeof (PlayList));
            var fileStream = new StreamWriter(fileName);

            serializer.Serialize(fileStream, playList);
        }

        public static PlayList LoadXml(this PlayList playList, string fileName)
        {
            var serializer = new XmlSerializer(typeof(PlayList));
            var fileStream = new StreamReader(fileName);

            return (PlayList) serializer.Deserialize(fileStream);
        }
    }
}
