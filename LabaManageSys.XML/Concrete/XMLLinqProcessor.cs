using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using LabaManageSys.XML.Abstract;
using LabaManageSys.XML.Models;

namespace LabaManageSys.XML.Concrete
{
    public class XMLLinqProcessor : IxmlProcessor
    {
        public void DownloadTasksToFile(IEnumerable<TaskXMLModel> tasks, string fullPath)
        {
            XDocument xdocument = new XDocument(new XElement(
                "Tasks", 
                tasks.Select(_ => new XElement(
                    "Task",
                    new XElement("Author", _.Author),
                    new XElement("Level", _.Level.ToString()),
                    new XElement("Topic", _.Topic),
                    new XElement("Name", _.Name),
                    new XElement("Text", _.Text)))));
            xdocument.Save(fullPath);
        }

        public IEnumerable<TaskXMLModel> UploadTasksFromFile(Stream inputStream)
        {
            var xdocument = XDocument.Load(inputStream);
            var tasks = new List<TaskXMLModel>();
            foreach (var node in xdocument.Root.Elements())
            {
                tasks.Add(new TaskXMLModel
                {
                    Author = node.Element("Author").Value,
                    Level = int.Parse(node.Element("Level").Value),
                    Topic = node.Element("Topic").Value,
                    Name = node.Element("Name").Value,
                    Text = node.Element("Text").Value
                });
            }

            inputStream.Dispose();
            return tasks;
        }
    }
}