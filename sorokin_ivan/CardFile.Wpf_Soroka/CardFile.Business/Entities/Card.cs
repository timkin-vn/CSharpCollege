using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string NameMedication { get; set; }
        public string TypeMedication { get; set; } 

        public DateTime DateManufacture { get; set; }

        public DateTime DateExpiration { get; set; }

        public int CountMedication { get; set; }

        public decimal PriceOneMedication { get; set; }
       

    

      
    }
}
