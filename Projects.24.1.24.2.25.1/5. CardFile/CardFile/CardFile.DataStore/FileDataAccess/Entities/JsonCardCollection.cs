using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCardCollection
    {
        /// <summary>
        /// Следующий свободный идентификатор для генерации ID в базе данных фильмов
        /// </summary>
        public int NextId { get; set; }

        /// <summary>
        /// Список всех сохраненных фильмов в формате JSON-сущностей
        /// </summary>
        public List<JsonCard> Cards { get; set; } = new List<JsonCard>();
    }
}
