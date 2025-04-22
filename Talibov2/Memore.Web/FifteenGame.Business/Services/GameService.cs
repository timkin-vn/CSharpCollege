using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{

    public class GameService
    {
        public void Initialize(GameModel model)
        {
            string[] vegetables = { "Tomato", "Carrot", "Potato", "Cucumber", "Broccoli", "Pepper", "Onion", "Garlic" };
            Random random = new Random();


            List<string> vegetablePairs = new List<string>();

            foreach (var vegetable in vegetables)
            {
                vegetablePairs.Add(vegetable);
                vegetablePairs.Add(vegetable);
            }



            vegetablePairs = vegetablePairs.OrderBy(x => random.Next()).ToList();


            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = vegetablePairs[row * GameModel.ColumnCount + column];

                }

            }
        }


        public bool CheckMatch(GameModel model)
        {

            if (model.Fistbuuton == null || model.Secondbuuton == null) 
            {
                return false;
            }

            else if (model.Fistbuuton == model.Secondbuuton 
                && (model.FisrsbuutonRowCol[0] != model.SecondbuutonRowCol[0] 
                ||model.FisrsbuutonRowCol[1] != model.SecondbuutonRowCol[1]))
            {
                model.CountTrueColorButton++;

                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    for (int column = 0; column < GameModel.ColumnCount; column++)
                    {
                        if ((row == model.FisrsbuutonRowCol[0] &&
                            column == model.FisrsbuutonRowCol[1]) || (row == model.SecondbuutonRowCol[0] &&
                            column == model.SecondbuutonRowCol[1]))
                        {

                            model[row, column] = "";


                        }
                       

                    }

                }
                
                
                return true;
            }
             return false; 
            
        }
        public bool IsGameOver(GameModel model)
        {
            if (model.CountTrueColorButton == 8)
            {
                model.CountTrueColorButton = 0;
                return true; 
            }
            

            return false;
        }

      
    }
}
