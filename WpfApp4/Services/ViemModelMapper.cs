using System.Collections.Generic;
using System.Windows.Media;
using WpfApp4.Core;
using WpfApp4.ViewModels;

namespace WpfApp4.Services
{
    public class ViewModelMapper
    {
        public List<MazeCellViewModel> MapToViewModels(GameEngine gameEngine)
        {
            var cells = new List<MazeCellViewModel>();

            for (int i = 0; i < gameEngine.MazeSize; i++)
            {
                for (int j = 0; j < gameEngine.MazeSize; j++)
                {
                    MazeCellViewModel cell;

                    if (i == gameEngine.PlayerRow && j == gameEngine.PlayerCol)
                    {
                        cell = CreatePlayerCell();
                    }
                    else if (i == gameEngine.ExitRow && j == gameEngine.ExitCol)
                    {
                        cell = CreateExitCell();
                    }
                    else
                    {
                        cell = CreateRegularCell(gameEngine.Maze[i, j]);
                    }

                    cells.Add(cell);
                }
            }

            return cells;
        }

        private MazeCellViewModel CreatePlayerCell()
        {
            return new MazeCellViewModel(
                symbol: "●",
                background: Brushes.LightGreen,
                foreground: Brushes.DarkGreen
            );
        }

        private MazeCellViewModel CreateExitCell()
        {
            return new MazeCellViewModel(
                symbol: "★",
                background: Brushes.Gold,
                foreground: Brushes.DarkOrange
            );
        }

        private MazeCellViewModel CreateRegularCell(CellType cellType)
        {
            if (cellType == CellType.Wall)
            {
                return new MazeCellViewModel("█", Brushes.DarkSlateGray, Brushes.White);
            }
            else if (cellType == CellType.Path)
            {
                return new MazeCellViewModel(" ", Brushes.White, Brushes.Black);
            }
            else if (cellType == CellType.Exit)
            {
                return new MazeCellViewModel("★", Brushes.Gold, Brushes.DarkOrange);
            }
            else
            {
                return new MazeCellViewModel(" ", Brushes.White, Brushes.Black);
            }
        }
    }
}