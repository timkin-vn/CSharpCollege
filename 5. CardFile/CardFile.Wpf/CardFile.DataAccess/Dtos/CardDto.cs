using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.Dtos
{
    using System;

   
    
        public class CardDto
        {
            // DTO = Data Transfer Object
            public int Id { get; set; }

            /// <summary>Имя</summary>
            public string FirstName { get; set; }

            /// <summary>Отчество</summary>
            public string MiddleName { get; set; }

            /// <summary>Фамилия</summary>
            public string LastName { get; set; }

            /// <summary>Дата рождения</summary>
            public DateTime BirthDate { get; set; }

            /// <summary>Пол</summary>
            public string Gender { get; set; }

            /// <summary>Адрес</summary>
            public string Address { get; set; }

            /// <summary>Диагноз</summary>
            public string Diagnosis { get; set; }

            /// <summary>Дата последнего приёма</summary>
            public DateTime? LastVisitDate { get; set; }

            /// <summary>Телефон</summary>
            public string PhoneNumber { get; set; }
             public string InsuranceNumber { get; set; }

        public CardDto Clone()
            {
                return new CardDto
                {
                    Id = Id,
                    FirstName = FirstName,
                    MiddleName = MiddleName,
                    LastName = LastName,
                    BirthDate = BirthDate,
                    Gender = Gender,
                    Address = Address,
                    Diagnosis = Diagnosis,
                    LastVisitDate = LastVisitDate,
                    PhoneNumber = PhoneNumber
                };
            }
        }
    }
