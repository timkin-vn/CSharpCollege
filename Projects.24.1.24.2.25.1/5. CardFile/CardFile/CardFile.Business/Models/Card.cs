using System;

namespace CardFile.Business.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string RealName { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }
        public string Team { get; set; }
        public string Role { get; set; }
        public decimal TotalEarnings { get; set; }
        public string Achievements { get; set; }
    }
}