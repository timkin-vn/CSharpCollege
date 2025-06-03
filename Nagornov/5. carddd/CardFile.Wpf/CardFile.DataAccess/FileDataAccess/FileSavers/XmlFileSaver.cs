using CardFile.Common.Infrastructure;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.FileDataAccess.DataEntites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class XmlFileSaver : IFileSaver
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    var serializer = new XmlSerializer(typeof(XmlCardCollection));
                    var xmlCollection = (XmlCardCollection)serializer.Deserialize(reader);

                    collection.ReplaceAll(xmlCollection.Cards.Select(c => Mapping.Mapper.Map<CardDto>(c)),
                    //new CardDto
                    //{
                    //    Id = c.Id,
                    //    FirstName = c.FirstName,
                    //    MiddleName = c.MiddleName,
                    //    LastName = c.LastName,
                    //    BirthDate = c.BirthDate,
                    //    Department = c.Department,
                    //    Position = c.Position,
                    //    EmploymentDate = c.EmploymentDate,
                    //    DismissalDate = c.DismissalDate,
                    //    Salary = c.Salary,
                    //}), 
                    xmlCollection.NextId);
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            var xmlCollection = new XmlCardCollection { NextId = collection.NextId, };
            xmlCollection.Cards.AddRange(collection.GetAll().Select(c => Mapping.Mapper.Map<XmlCard>(c)));
            //new XmlCard
            //{
            //    Id = c.Id,
            //    FirstName = c.FirstName,
            //    MiddleName = c.MiddleName,
            //    LastName = c.LastName,
            //    BirthDate = c.BirthDate,
            //    Department = c.Department,
            //    Position = c.Position,
            //    EmploymentDate = c.EmploymentDate,
            //    DismissalDate = c.DismissalDate,
            //    Salary = c.Salary,
            //}));

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    var serializer = new XmlSerializer(typeof(XmlCardCollection));
                    serializer.Serialize(writer, xmlCollection);
                }
            }
        }
    }
}
