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
                Title = "Макароны Спагетти Makfa",
                EXP = new DateTime(2026, 11, 7),
                Fabricator = "Makfa",
                Section = "Бакалея",
                Count = 8,
                Price = 79m,
            },
            new CardDto
            {
                Id = 2,
                Title = "Батон Сормовский в нарезке",
                EXP = new DateTime(2024, 12, 15),
                Fabricator = "Сормовский хлеб",
                Section = "Хлеб и выпечка",
                Count = 15,
                Price = 30m,
            },
            new CardDto
            {
                Id = 3,
                Title = "Молоко 2,5% пастеризованное",
                EXP = new DateTime(2024, 12, 19),
                Fabricator = "Княгинино",
                Section = "Молочная продукция",
                Count = 6,
                Price = 52m,
            },
            new CardDto
            {
                Id = 4,
                Title = "Пирожное песочное глазированное",
                EXP = new DateTime(2024, 12, 29),
                Fabricator = "Сормовский хлеб",
                Section = "Хлеб и выпечка",
                Count = 5,
                Price = 37m,
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
