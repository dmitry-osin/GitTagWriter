using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using GitTagWriter.Contracts;
using GitTagWriter.Model;

namespace GitTagWriter.Service
{
    public class TagSerializeService : ISerializer<Tag>, IDataStore
    {
        /// <summary>
        /// Метод сериализует список тегов
        /// </summary>
        /// <param name="list">Коллекция для сериализации</param>
        /// <returns>XML как строку</returns>
        public string Serialize(IEnumerable<Tag> list)
        {
            var document = new XDocument();
            var root = new XElement("root");
            foreach (var tag in list)
            {
                root.Add(new XElement("tag", new XAttribute("name", tag.Id), new XAttribute("date", tag.Commit.Date.ToString("R"))));
            }
            document.Add(root);
            return document.ToString();
        }

        /// <summary>
        /// Метод сохраняет сериализованные данные на жесткий диск
        /// </summary>
        /// <param name="path">Путь сохранения файла</param>
        /// <param name="data">Данные для сохранения</param>
        public void SaveOnDisk(string path, string data)
        {
            File.WriteAllText(path, data);
        }
    }
}