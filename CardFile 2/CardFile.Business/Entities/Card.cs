﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Entities
{
    public class Card
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal HeightAmount { get; set; }

        public int Weight { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
    }
}
