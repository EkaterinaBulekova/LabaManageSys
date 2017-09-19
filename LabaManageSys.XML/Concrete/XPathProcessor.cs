using System.Collections.Generic;
using System.IO;
using System.Xml;
using LabaManageSys.XML.Abstract;
using LabaManageSys.XML.Models;

namespace LabaManageSys.XML.Concrete
{
    public class XPathProcessor : IxmlProcessor
    {
        public void DownloadTasksToFile(IEnumerable<TaskXMLModel> tasks, string fullPath)
        {
            XmlDocument xmldocument = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmldocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = xmldocument.DocumentElement;
            xmldocument.InsertBefore(xmlDeclaration, root);
            XmlElement eltasks = xmldocument.CreateElement(string.Empty, "Tasks", string.Empty);
            xmldocument.AppendChild(eltasks);
            foreach (var task in tasks)
            {
                XmlElement eltask = xmldocument.CreateElement(string.Empty, "Task", string.Empty);
                eltasks.AppendChild(eltask);
                XmlElement elauthor = xmldocument.CreateElement(string.Empty, "Author", string.Empty);
                XmlText author = xmldocument.CreateTextNode(task.Author);
                elauthor.AppendChild(author);
                eltask.AppendChild(elauthor);
                XmlElement ellevel = xmldocument.CreateElement(string.Empty, "Level", string.Empty);
                XmlText level = xmldocument.CreateTextNode(task.Level.ToString());
                ellevel.AppendChild(level);
                eltask.AppendChild(ellevel);
                XmlElement eltopic = xmldocument.CreateElement(string.Empty, "Topic", string.Empty);
                XmlText topic = xmldocument.CreateTextNode(task.Topic);
                eltopic.AppendChild(topic);
                eltask.AppendChild(eltopic);
                XmlElement elname = xmldocument.CreateElement(string.Empty, "Name", string.Empty);
                XmlText name = xmldocument.CreateTextNode(task.Name);
                eltopic.AppendChild(name);
                eltask.AppendChild(elname);
                XmlElement eltext = xmldocument.CreateElement(string.Empty, "Level", string.Empty);
                XmlText text = xmldocument.CreateTextNode(task.Text);
                eltext.AppendChild(text);
                eltask.AppendChild(eltext);
            }

            xmldocument.Save(fullPath);
        }

        public IEnumerable<TaskXMLModel> UploadTasksFromFile(Stream inputStream)
        {
            List<TaskXMLModel> tasks = new List<TaskXMLModel>();
            var xmldocument = new XmlDocument();
            xmldocument.Load(inputStream);

            foreach (XmlNode node in xmldocument.DocumentElement.SelectNodes("Task"))
            {
                tasks.Add(new TaskXMLModel
                {
                    Author = node.SelectSingleNode("Author").InnerText,
                    Level = int.Parse(node.SelectSingleNode("Level").InnerText),
                    Topic = node.SelectSingleNode("Topic").InnerText,
                    Name = node.SelectSingleNode("Name").InnerText,
                    Text = node.SelectSingleNode("Text").InnerText
                });
            }

            inputStream.Dispose();
            return tasks;
        }
    }
}