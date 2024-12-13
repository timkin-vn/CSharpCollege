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

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public DateTime DatePurchase { get; set; }

        public decimal Price { get; set; }

        public int Mileage { get; set; }
    }
}
