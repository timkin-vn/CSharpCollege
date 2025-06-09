﻿using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.DataCollection
{
    public class CardCollection
    {
        private List<CardDto> _cards = new List<CardDto>
        {
            new CardDto
            {
                Id = 1,
                FirstName = "Канье",
                MiddleName = "Пабло",
                LastName = "Вест",
                BirthDate = new DateTime(2002, 1, 15),
                PaymentAmount = 69m,
                Circulation = 5,
                Booktitle = "Иецус",
                Publisher = "Канье",
                Year = "0"
            },
            new CardDto
            {
                Id = 2,
                FirstName = "Лев",
                MiddleName = "Николаевич",
                LastName = "Толстой",
                BirthDate = new DateTime(1828, 9, 9),
                PaymentAmount = 9999m,
                Circulation = 3,
                Booktitle = "Война и Мир",
                Publisher = "Мондадори",
                Year = "1867"
            },
            new CardDto
            {
                Id = 3,
                FirstName = "Джон",
                MiddleName = "Блуе",
                LastName = "Аркайв",
                BirthDate = new DateTime(1990, 2, 1),
                PaymentAmount = 0m,
                Circulation = 0,
                Booktitle = "БлуАка",
                Publisher = "Иостар",
                Year = "2020"
            },
            new CardDto
            {
                Id = 4,
                FirstName = "Марк",
                MiddleName = " ",
                LastName = "Твейн",
                BirthDate = new DateTime(1999, 5, 8),
                PaymentAmount = 800m,
                Circulation = 1,
                Booktitle = "идк",
                Publisher = "я",
                Year = "1999"
            },
        };

        internal int CurrentId = 5;

        public CardCollection()
        {
            MapperInitialize.Initialize();
        }

        public IEnumerable<CardDto> GetAll()
        {
            return _cards.Select(c => c.Clone());
        }

        public CardDto Get(int id)
        {
            return _cards.FirstOrDefault(c => c.Id == id)?.Clone();
        }

        public bool Update(CardDto card)
        {
            if (card.Id == 0)
            {
                return false;
            }

            var existingCard = _cards.FirstOrDefault(c => c.Id == card.Id);
            if (existingCard == null)
            {
                return false;
            }

            existingCard.Update(card);
            return true;
        }

        public int Save(CardDto card)
        {
            if (card.Id == 0)
            {
                var newCard = card.Clone();
                newCard.Id = CurrentId++;
                _cards.Add(newCard);
                return newCard.Id;
            }

            var existingCard = _cards.FirstOrDefault(c => c.Id == card.Id);
            if (existingCard == null)
            {
                return -1;
            }

            existingCard.Update(card);
            return card.Id;
        }

        public bool Delete(int id)
        {
            var existingCard = _cards.FirstOrDefault(c => c.Id == id);

            if (existingCard == null)
            {
                return false;
            }

            _cards.Remove(existingCard);
            return true;
        }

        internal void ReplaceCollection(IEnumerable<CardDto> collection)
        {
            _cards.Clear();
            _cards.AddRange(collection);
            CurrentId = _cards.Max(c => c.Id) + 1;
        }

        internal void ReplaceCollection(IEnumerable<CardDto> collection, int currentId)
        {
            _cards.Clear();
            _cards.AddRange(collection);
            CurrentId = currentId;
        }
    }
}
