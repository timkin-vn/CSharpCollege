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
    { 
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


        public bool Search_for_parents(GameModel model, int colum ,int  rowe)
        {

            if (model.Fisra == 0)
            {
                model.OneRowCol = new int[2] { rowe, colum }; ;
                model.Onebuuton = model[rowe, colum];
                model.Fisra++;
                


            }
            else if (model.Fisra == 1)
            {
                model.Twobuuton = model[rowe, colum];
                model.TwoRowCol = new int[2] { rowe, colum };



                model.Fisra = 0;
                

            }
            if (model.Onebuuton == null || model.Twobuuton == null)
            {
                    return false;
            }
            else if (model.Onebuuton == model.Twobuuton && (model.OneRowCol[0] != model.TwoRowCol[0]
                    || model.OneRowCol[1] != model.TwoRowCol[1]))
            {
                model.lower++;

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
                

                return true;
            }
            else
            {
                return false;
            }




        }  

        

        public bool IsGameOver(GameModel model)
        {
            if (model.lower == 8)
            {
                model.TwoRowCol[0] = 4;
                model.TwoRowCol[1] = 4;
                model.OneRowCol[0] = 4;
                model.OneRowCol[1] = 4;
                model.lower = 0;
                return true; 
            }


            return false;
        }

      
    }
}
