﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.StorageEntities
{
    [Serializable]
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [XmlElement("BirthDate")]
        public long BirthDateTicks
        {
            get => BirthDate.Ticks;
            set => BirthDate = new DateTime(value);
        }

        [XmlIgnore]
        public DateTime BirthDate { get; set; }

        public decimal HeightAmount { get; set; }

        public int Weight { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
    }
}
