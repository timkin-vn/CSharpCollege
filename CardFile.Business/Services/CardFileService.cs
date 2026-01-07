using CardFile.Business.Models;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.FileDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Services
{
    public class CardFileService
    {
        private CardCollection _cardCollection = new CardCollection();

        public IEnumerable<Card> GetAll()
        {
            var dtos = _cardCollection.GetAll();
            return dtos.Select(FromDto).ToList();
        }

        public int Save(Card card)
        {
            return _cardCollection.Save(ToDto(card));
        }

        public bool Delete(int cardId)
        {
            return _cardCollection.Delete(cardId);
        }

        public void SaveToFile(string fileName)
        {
            var fileDataService = new FileDataService();
            fileDataService.SaveToFile(fileName, _cardCollection);
        }

        public void OpenFromFile(string fileName)
        {
            var fileDataService = new FileDataService();
            fileDataService.OpenFromFile(fileName, _cardCollection);
        }

        private Card FromDto(CardDto dto)
        {
            return new Card
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                BirthDate = dto.BirthDate,
                RegistrationDate = dto.RegistrationDate,
                Autor = dto.Autor,
                Genre = dto.Genre,
                Book = dto.Book,
                GetDate = dto.GetDate,
                RefundDate = dto.RefundDate,
                Collection = dto.Collection,
            };
        }

        private CardDto ToDto(Card card)
        {
            return new CardDto
            {
                Id = card.Id,
                FirstName = card.FirstName,
                LastName = card.LastName,
                MiddleName = card.MiddleName,
                BirthDate = card.BirthDate,
                RegistrationDate = card.RegistrationDate,
                Autor = card.Autor,
                Genre = card.Genre,
                Book = card.Book,
                GetDate = card.GetDate,
                RefundDate = card.RefundDate,
                Collection = card.Collection,
            };
        }
    }
}
