using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{

    public class GameService
    { int countFamili = 0;
        public void Initialize(GameModel model)
        {
            string[] cardAnimal = { "C#", "Python", "C++", "Java", "Rust", "JavaScript", "Pascal", "GoLang" };
            Random random = new Random();


            List<string> cardAmimalPairs = new List<string>();

            foreach (var color in cardAnimal)
            {
                cardAmimalPairs.Add(color);
                cardAmimalPairs.Add(color);
            }



            cardAmimalPairs = cardAmimalPairs.OrderBy(x => random.Next()).ToList();


            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = cardAmimalPairs[row * GameModel.ColumnCount + column];

                }

            }
        }


        public bool Search_for_parents(GameModel model, int[] colrow)
        {

            if (model.Fisra == 0)
            {
                model.OneRowCol = colrow;
                model.Onebuuton = model[colrow[0], colrow[1]];
                model.Fisra++;
                


            }
            else if (model.Fisra == 1)
            {
                model.Twobuuton = model[colrow[0], colrow[1]];
                model.TwoRowCol = colrow;



                model.Fisra = 0;
                

            }
            if (model.Onebuuton == null || model.Twobuuton == null)
            {
                    return false;
            }
            else if (model.Onebuuton == model.Twobuuton && (model.OneRowCol[0] != model.TwoRowCol[0]
                    || model.OneRowCol[1] != model.TwoRowCol[1]))
            {
                countFamili++;

                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    for (int column = 0; column < GameModel.ColumnCount; column++)
                    {
                        if ((row == model.OneRowCol[0] &&
                            column == model.OneRowCol[1]) || (row == model.TwoRowCol[0] &&
                            column == model.TwoRowCol[1]))
                        {

                            model[row, column] = "";


                        }
                        else
                        {
                            model[row, column] = model[row, column];
                        }

                    }


                }
                model.TwoRowCol[0] = 4;
                model.TwoRowCol[1] = 4;
                model.OneRowCol[0] = 4;
                model.OneRowCol[1] = 4;

                return true;
            }
            else
            {
                return false;
            }




        }  

        

        public bool IsGameOver(GameModel model)
        {
            if (countFamili == 8)
            {
                model.TwoRowCol[0] = 4;
                model.TwoRowCol[1] = 4;
                model.OneRowCol[0] = 4;
                model.OneRowCol[1] = 4;
                countFamili = 0;
                return true; 
            }


            return false;
        }

      
    }
}
