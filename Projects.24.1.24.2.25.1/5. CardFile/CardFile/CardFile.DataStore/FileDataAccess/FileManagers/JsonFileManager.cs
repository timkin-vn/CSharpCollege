using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class JsonFileManager : IFileManager
    {
        public void SaveToFile(string fileName, CardCollection collection)
        {
            // 1. Получаем все DTO из коллекции
            var dtos = collection.GetAll();

            // 2. Маппим DTO в сущности для JSON (через AutoMapper)
            var jsonCards = dtos.Select(d => Mapping.Mapper.Map<JsonCard>(d)).ToList();

            // 3. Создаем объект-контейнер для сохранения
            var storage = new JsonCardCollection
            {
                NextId = collection.NextId,
                Cards = jsonCards
            };

            // 4. Сериализуем и записываем в файл
            string json = JsonConvert.SerializeObject(storage, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        public void OpenFromFile(string fileName, CardCollection collection)
        {
            if (!File.Exists(fileName)) return;

            // 1. Читаем текст из файла
            string json = File.ReadAllText(fileName);

            // 2. Десериализуем в объект-контейнер
            var storage = JsonConvert.DeserializeObject<JsonCardCollection>(json);

            if (storage != null)
            {
                // 3. Маппим сущности JSON обратно в DTO
                var dtos = storage.Cards.Select(j => Mapping.Mapper.Map<CardDto>(j));

                // 4. Обновляем основную коллекцию
                collection.ReplaceAll(dtos, storage.NextId);
            }
        }
    }
}
