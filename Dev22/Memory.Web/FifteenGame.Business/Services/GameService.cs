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
    { int countPar = 0;
        public void Initialize(GameModel model)
        {
            string[] colors = { "Coal", "Copper", "Lazulit", "Iron", "Gold", "Diamond", "Sapphire", "Emerald" };
            Random random = new Random();


            List<string> colorPairs = new List<string>();

            foreach (var color in colors)
            {
                colorPairs.Add(color);
                colorPairs.Add(color);
            }



            colorPairs = colorPairs.OrderBy(x => random.Next()).ToList();


            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = colorPairs[row * GameModel.ColumnCount + column];

                }

            }
        }


        public bool CheckMatch(GameModel model, int col,int row)
        {
            if (model.Clickbutton == 0)
            {
                model.OneColumnRow[0] =row;
                model.OneColumnRow[1] = col;
               
                model.Clickbutton++;
               

            }
            else if (model.Clickbutton == 1)
            {

                model.TwoColumnRow[0] = row;
                model.TwoColumnRow[1] = col;



                model.Clickbutton = 0;

            }

            if (model[model.OneColumnRow[0], model.OneColumnRow[1]] == null || model[model.TwoColumnRow[0], model.TwoColumnRow[1]] == null) 
            {
                return false;
            }

            else if ((model[model.OneColumnRow[0], model.OneColumnRow[1]] == model[model.TwoColumnRow[0], model.TwoColumnRow[1]])&&(model.OneColumnRow[0] != model.TwoColumnRow[0]
                ||model.OneColumnRow[1] != model.TwoColumnRow[1]))
            {
                model.CountMove++;

                for (int rowe = 0; rowe < GameModel.RowCount; rowe++)
                {
                    for (int column = 0; column < GameModel.ColumnCount; column++)
                    {
                        if ((rowe == model.OneColumnRow[0] &&
                            column == model.OneColumnRow[1]) || (rowe == model.TwoColumnRow[0] &&
                            column == model.TwoColumnRow[1]))
                        {

                            model[rowe, column] = "";


                        }
                       

                    }

                }
                model.TwoColumnRow[0] = 0;
                model.TwoColumnRow[1] = 0;
                model.OneColumnRow[0] = 0;
                model.OneColumnRow[1] = 0;

                return true;
            }
            else
            { 
                return false; 
            }
       }  

        

        public bool IsGameOver(GameModel model)
        {
            if (model.CountMove == 8)
            {
               
                model.CountMove = 0;
                return true; 
            }


            return false;
        }

      
    }
}
