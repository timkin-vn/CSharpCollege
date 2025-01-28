using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//представления карточки
namespace MemoryGame.Model
{
    public class Card
    {
        public int Id { get; }
        public string ImagePath { get; }
        public bool IsFlipped { get; set; }
        public bool IsMatched { get; set; }

        public Card(int id, string imagePath)
        {
            Id = id;
            ImagePath = imagePath;
            IsFlipped = false;
            IsMatched = false;
        }
    }
}
