using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdryBotGUI
{
    static class SimpleCommands
    {
        public static SerializableDictionary<string, string> Commands = new SerializableDictionary<string, string>();

        public static async Task Execute(SocketCommandContext context, string message)
        {
            foreach(KeyValuePair<string, string> s in Commands)
            {
                if (s.Key == message)
                {
                    await context.Channel.SendMessageAsync(EvaluateVariables(context, s.Value).Result);
                }
            }
        }

        public static async Task<string> EvaluateVariables(SocketCommandContext context, string message)
        {
            message = message.Replace("{user}", context.User.ToString());
            message = message.Replace("{muser}", context.User.Mention);
            message = message.Replace("{uid}", context.User.Id.ToString());
            if (!context.IsPrivate)
            {
                message = message.Replace("{owner}", context.Guild.Owner.ToString());
                message = message.Replace("{membercount}", context.Guild.MemberCount.ToString());
                message = message.Replace("{description}", context.Guild.Description);
            }
            return message;
        }

        public static void FromFile(string fileName)
        {
            string serialized = File.ReadAllText(fileName);
            XmlSerializer xml_serializer = new XmlSerializer(typeof(SerializableDictionary<string, string>));
            SerializableDictionary<string, string> cfg;
            using (StringReader string_reader = new StringReader(serialized))
            {
                cfg = (SerializableDictionary<string, string>)(xml_serializer.Deserialize(string_reader));
            }
            Commands = cfg;
        }

        public static void Save(string fileName)
        {
            XmlSerializer xml_serializer = new XmlSerializer(typeof(SerializableDictionary<string, string>));
            using (StringWriter string_writer = new StringWriter())
            {
                // Serialize.
                xml_serializer.Serialize(string_writer, Commands);

                File.WriteAllText(fileName, string_writer.ToString());
            }
        }
    }
}
