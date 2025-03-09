using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MainForm : Form
    {
        private const int CellSize = 30;
        private const int GridWidth = 10;
        private const int GridHeight = 10;
        private const int NumberOfMines = 10;
        private Cell[,] grid = new Cell[GridWidth, GridHeight];
        private bool isGameOver = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeGrid();
            panel1.Size = new Size(GridWidth * CellSize, GridHeight * CellSize);
            panel1.Paint += Panel1_Paint;
            panel1.MouseDown += Panel1_MouseDown;
        }

        private void InitializeGrid()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    grid[x, y] = new Cell();
                }
            }
            PlaceMines();
            CalculateNumbers();
            isGameOver = false;
        }

        private void PlaceMines()
        {
            Random random = new Random();
            int minesPlaced = 0;
            while (minesPlaced < NumberOfMines)
            {
                int x = random.Next(GridWidth);
                int y = random.Next(GridHeight);
                if (!grid[x, y].IsMine)
                {
                    grid[x, y].IsMine = true;
                    minesPlaced++;
                }
            }
        }

        private void CalculateNumbers()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    if (!grid[x, y].IsMine)
                    {
                        grid[x, y].Number = CountMinesAround(x, y);
                    }
                }
            }
        }

        private int CountMinesAround(int x, int y)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    int nx = x + i;
                    int ny = y + j;
                    if (nx >= 0 && nx < GridWidth && ny >= 0 && ny < GridHeight && grid[nx, ny].IsMine)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    Brush brush = Brushes.LightGray;
                    if (grid[x, y].IsRevealed)
                    {
                        brush = grid[x, y].IsMine ? Brushes.Red : Brushes.White;
                    }
                    e.Graphics.FillRectangle(brush, x * CellSize, y * CellSize, CellSize, CellSize);
                    e.Graphics.DrawRectangle(Pens.Gray, x * CellSize, y * CellSize, CellSize, CellSize);

                    if (grid[x, y].IsRevealed && !grid[x, y].IsMine && grid[x, y].Number > 0)
                    {
                        StringFormat sf = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        e.Graphics.DrawString(grid[x, y].Number.ToString(), this.Font, Brushes.Black, new RectangleF(x * CellSize, y * CellSize, CellSize, CellSize), sf);
                    }
                    else if (grid[x, y].IsFlagged)
                    {
                        e.Graphics.DrawString("F", this.Font, Brushes.Black, new RectangleF(x * CellSize, y * CellSize, CellSize, CellSize), new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        });
                    }
                }
            }
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (isGameOver) return;

            int x = e.X / CellSize;
            int y = e.Y / CellSize;
            if (x >= 0 && x < GridWidth && y >= 0 && y < GridHeight)
            {
                if (e.Button == MouseButtons.Left)
                {
                    RevealCell(x, y);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    ToggleFlag(x, y);
                }
                panel1.Invalidate();
            }

            if (CheckWin())
            {
                MessageBox.Show("Вы выиграли!", "Поздравляем", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isGameOver = true;
            }
        }

        private void RevealCell(int x, int y)
        {
            if (grid[x, y].IsFlagged || grid[x, y].IsRevealed) return;

            grid[x, y].IsRevealed = true;

            if (grid[x, y].IsMine)
            {
                RevealAllMines();
                MessageBox.Show("Игра окончена!", "Вы проиграли", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                isGameOver = true;
                return;
            }

            if (grid[x, y].Number == 0)
            {
                RevealAdjacentCells(x, y);
            }
        }

        private void RevealAllMines()
        {
            for (int i = 0; i < GridWidth; i++)
            {
                for (int j = 0; j < GridHeight; j++)
                {
                    if (grid[i, j].IsMine)
                    {
                        grid[i, j].IsRevealed = true;
                    }
                }
            }
        }

        private void RevealAdjacentCells(int x, int y)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int nx = x + i;
                    int ny = y + j;
                    if (nx >= 0 && nx < GridWidth && ny >= 0 && ny < GridHeight)
                    {
                        RevealCell(nx, ny);
                    }
                }
            }
        }

        private void ToggleFlag(int x, int y)
        {
            if (grid[x, y].IsRevealed) return;

            grid[x, y].IsFlagged = !grid[x, y].IsFlagged;
        }

        private bool CheckWin()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    if (!grid[x, y].IsMine && !grid[x, y].IsRevealed)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void ButtonRestart_Click(object sender, EventArgs e)
        {
            InitializeGrid();
            panel1.Invalidate();
        }
    }
}