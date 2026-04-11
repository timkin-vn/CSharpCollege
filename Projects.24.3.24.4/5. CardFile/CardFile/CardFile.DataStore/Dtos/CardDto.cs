using CardFile.Common.Infrastructure;
using System;

namespace CardFile.DataStore.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public string DeliveryMethod { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public decimal TotalAmount { get; set; }

        internal CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
        }
    }
}
