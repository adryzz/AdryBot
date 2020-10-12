using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Drawing;
using Discord;
using Color = System.Drawing.Color;

namespace AdryBotGUI
{
    [Serializable()]
    public class Configuration
    {
        public string Token = "";
        public string BotOwner = "";
        public string CmdPrefix = ".";
        public string Game = "";
        public string Url = "";
        public Color Color = Color.FromArgb(0, 0, 0);
        public UserStatus Status = UserStatus.Online;

        public Configuration()
        {

        }

        public static Configuration FromFile(string fileName)
        {
            string serialized = File.ReadAllText(fileName);
            XmlSerializer xml_serializer = new XmlSerializer(typeof(Configuration));
            Configuration cfg;
            using (StringReader string_reader = new StringReader(serialized))
            {
                cfg = (Configuration)(xml_serializer.Deserialize(string_reader));
            }
            return cfg;
        }

        public void Save(string fileName)
        {
            XmlSerializer xml_serializer = new XmlSerializer(typeof(Configuration));
            using (StringWriter string_writer = new StringWriter())
            {
                // Serialize.
                xml_serializer.Serialize(string_writer, this);

                File.WriteAllText(fileName, string_writer.ToString());
            }
        }
    }
}
