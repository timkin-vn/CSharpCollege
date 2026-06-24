using CardFile.Business.Infrastructure;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Services
{
    public class CardFileService
    {
        private readonly CardCollection _collection = new CardCollection();

        public CardFileService()
        {
            MapperInitialization.PreRegister();
        }

        public IEnumerable<Card> GetAll()
        {
            var cards = _collection.GetAll();
            return cards.Select(FromDto).ToList();
        }

        public int Save(Card card)
        {
            return _collection.Save(ToDto(card));
        }

        public bool Delete(int cardId)
        {
            return _collection.Delete(cardId);
        }

        public void SaveToFile(string fileName)
        {
            var fileDataService = new FileDataService();
            fileDataService.SaveToFile(fileName, _collection);
        }

        public void OpenFromFile(string fileName)
        {
            var fileDataService = new FileDataService();
            fileDataService.OpenFromFile(fileName, _collection);
        }

        private Card FromDto(CardDto card)
        {
            if (card == null)
                return null;

            return new Card
            {
                Id = card.Id,
                FirstName = card.FirstName,
                MiddleName = card.MiddleName,
                LastName = card.LastName,
                BirthDate = card.BirthDate,
                DeathDate = card.DeathDate,
                CauseOfDeath = card.CauseOfDeath,
                PlaceOfDeath = card.PlaceOfDeath,
                AdmissionDate = card.AdmissionDate,
                SectionNumber = card.SectionNumber,
                IsReleased = card.IsReleased
            };
        }

        private CardDto ToDto(Card card)
        {
            if (card == null)
                return null;

            return new CardDto
            {
                Id = card.Id,
                FirstName = card.FirstName,
                MiddleName = card.MiddleName,
                LastName = card.LastName,
                BirthDate = card.BirthDate,
                DeathDate = card.DeathDate,
                CauseOfDeath = card.CauseOfDeath,
                PlaceOfDeath = card.PlaceOfDeath,
                AdmissionDate = card.AdmissionDate,
                SectionNumber = card.SectionNumber,
                IsReleased = card.IsReleased
            };
        }
    }
}
