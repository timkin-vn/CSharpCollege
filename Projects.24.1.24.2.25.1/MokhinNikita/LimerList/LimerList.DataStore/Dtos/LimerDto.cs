using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimerList.Common.Infrastructure;

namespace LimerList.DataStore.Dtos
{
    public class LimerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Group { get; set; }
        public LimerDto Clone()
        {
            return Mapping.Mapper.Map<LimerDto>(this);
        }
    }
}
