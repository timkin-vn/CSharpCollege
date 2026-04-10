using CardFile.Business.Infrastructure;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CardFile.Business.Services
{
    public class CardFileService
    {
        private CardCollection _collection = new CardCollection();

        public CardFileService()
        {
            MapperInitialization.PreRegister();
        }

        public IEnumerable<Card> GetAll()
        {
            var cardDtos = _collection.GetAll();
            return cardDtos.Select(FromDto).ToList();
        }

        public int Save(Card card)
        {
            return _collection.Save(ToDto(card));
        }

        public bool Delete(int cardId)
        {
            return false;
        }

        private Card FromDto(CardDto card)
        {
            return Mapping.Mapper.Map<Card>(card);
        }

        private CardDto ToDto(Card card)
        {
            return Mapping.Mapper.Map<CardDto>(card);
        }
        public void SaveToFile(string fileName, int formatIndex)
        {
            var data = _collection.GetAll().ToList();

            switch (formatIndex)
            {
                case 1:
                    XmlSerializer xmlFormatter = new XmlSerializer(typeof(List<CardDto>));
                    using (FileStream fs = new FileStream(fileName, FileMode.Create))
                    {
                        xmlFormatter.Serialize(fs, data);
                    }
                    break;

                case 2:
                    string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(fileName, json);
                    break;

                case 3:
                    var lines = data.Select(c => $"{c.Brand} {c.ModelName} ({c.Year}) - VIN: {c.VinCode}");
                    File.WriteAllLines(fileName, lines);
                    break;
            }
        }

        public void LoadFromFile(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();

            switch (extension)
            {
                case ".xml":
                    XmlSerializer xmlFormatter = new XmlSerializer(typeof(List<CardDto>));
                    using (FileStream fs = new FileStream(fileName, FileMode.Open))
                    {
                        var data = (List<CardDto>)xmlFormatter.Deserialize(fs);
                        UpdateCollection(data);
                    }
                    break;

                case ".json":
                    string json = File.ReadAllText(fileName);
                    var jsonData = JsonConvert.DeserializeObject<List<CardDto>>(json);
                    UpdateCollection(jsonData);
                    break;

                default:
                    throw new Exception("формат не подходит!");
            }
        }
        private void UpdateCollection(List<CardDto> data)
        {
            if (data == null) return;

            foreach (var item in data)
            {
                _collection.Save(item);
            }
        }
    }
}
