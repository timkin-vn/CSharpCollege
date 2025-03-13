﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIneSweepper.Bisiness.Model;

namespace MIneSweepper.Bisiness.Services
{
    public class MineSevicer
    {
        public void Iniziallize(GameModel model, int mines)
        {
            

            for (int x = 0; x < GameModel.RowCount; x++)
            {
                for (int y = 0; y < GameModel.ColumnCount; y++)
                {
                    model[x, y] = new CellsModel();
                }
            }
            PlaceMines(mines, model);
            CountNeighborMines(model);
            model.RedFlags = mines;
            model.MineCount = mines;
            model.CountRevealed = 0;

        }
        public void PlaceMines(int mineCount, GameModel model)
        {
            int placedMines = 0;
            var random = new Random();
            while (placedMines < mineCount)
            {
                int x = random.Next(GameModel.RowCount);
                int y = random.Next(GameModel.ColumnCount);
                if (!model[x, y].IsMine)
                {
                    model[x, y].IsMine = true;
                    placedMines++;
                }
            }
        }

        public void CountNeighborMines(GameModel model)
        {
            for (int x = 0; x < GameModel.RowCount; x++)
            {
                for (int y = 0; y < GameModel.ColumnCount; y++)
                {
                    if (model[x, y].IsMine)
                    {
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            for (int dy = -1; dy <= 1; dy++)
                            {
                                int nx = x + dx;
                                int ny = y + dy;
                                if (nx >= 0 && nx < GameModel.RowCount && ny >= 0 && ny < GameModel.ColumnCount && !model[nx, ny].IsMine)
                                {
                                    model[nx, ny].NeightborMines++;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void OpenAllMines(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column].IsMine)
                    {

                        model[row, column].IsRevealed = true;
                    }
                }
            }
        }
        public bool IsGameOver(GameModel model)
        {

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model.CountRevealed == (GameModel.RowCount * GameModel.ColumnCount) - model.MineCount)
                    {
                        return true;
                    }
                    if (model[row, column].IsMine && model[row, column].IsRevealed)
                    {
                        OpenAllMines(model);
                        return true;

                    }
                }
            }
            return false;
        }
        public void issFlaged(GameModel model, int x, int y)
        {

            if (model.RedFlags > 0&&!model[x,y].Isflag)
            {
                model.RedFlags--;
                model[x, y].Isflag=true;
            }
            else if(model[x, y].Isflag) 
            {
                model[x, y].Isflag = false;
                model.RedFlags++;
            }
        }

        public void RevealCell(int x, int y, GameModel model)
        {
             
            if (x < 0 || x >= GameModel.RowCount || y < 0 || y >= GameModel.ColumnCount || model[x, y].IsRevealed ||model[x,y].Isflag|| IsGameOver(model))
                return;

            model[x, y].IsRevealed = true;
            model.CountRevealed++;
            
                if (model[x, y].IsMine)
            {
                OpenAllMines(model);
                Console.WriteLine("Game Over! You hit a mine.");
                return;
            }

                if (model[x, y].NeightborMines == 0)
                {

                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            RevealCell(x + dx, y + dy, model);
                        }
                    }

                }
        
        }

    }
}
