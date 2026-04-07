using System;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlLetter
    {
        [XmlAttribute("Id")] public int Id { get; set; }
        [XmlAttribute("Sender")] public string Sender { get; set; }
        [XmlAttribute("Receiver")] public string Receiver { get; set; }
        [XmlAttribute("Subject")] public string Subject { get; set; }
        [XmlAttribute("Body")] public string Body { get; set; }

        [XmlIgnore] public DateTime Date { get; set; }
        [XmlAttribute("Date")] public string DateText { get => Date.ToShortDateString(); set => Date = DateTime.Parse(value); }

        [XmlAttribute("IsRead")] public bool IsRead { get; set; }
    }
}