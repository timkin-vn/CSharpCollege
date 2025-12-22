using Pacman.Common.Dtos;
using Pacman.Common.Enums;
using Pacman.Common.Services;
using System.Collections.Generic;

namespace Pacman.Business.Services
{
    public class GameService : IGameService
    {
        public MapDto GetLevel(int level)
        {
            

            string[] levelDesign = new string[]
            {
                 "WWWWWWWWWWWWWWWWWWW",
"W........W........W",
"W.WW.WWW.W.WWW.WW.W",
"W.................W",
"W.WW.W.WWWWW.W.WW.W",
"W....W...W...W....W",
"WWWW.WWW W WWW.WWWW",
"   W.W       W.W   ",
"WWWW.W WW WW W.WWWW",
"      G G G G      ",
"WWWW.W WWWWW W.WWWW",
"W........W........W",
"W.WW.WWW.W.WWW.WW.W",
"W..W.....P.....W..W",
"WW.W.W.WWWWW.W.W.WW",
"W....W.......W....W",
"W.WWWWWW.W.WWWWWW.W",
"W.................W",
"WWWWWWWWWWWWWWWWWWW"

            };

            int height = levelDesign.Length;
            int width = levelDesign[0].Length;

            var cells = new List<CellType>();

            foreach (var row in levelDesign)
            {
                foreach (var charTile in row)
                {
                    switch (charTile)
                    {
                        case 'W': cells.Add(CellType.Wall); break;
                        case '.': cells.Add(CellType.Coin); break;
                        case 'P': cells.Add(CellType.Pacman); break;
                        case 'G': cells.Add(CellType.Ghost); break;
                        default: cells.Add(CellType.Empty); break; 
                    }
                }
            }

            return new MapDto
            {
                Width = width,
                Height = height,
                Cells = cells.ToArray()
            };
        }
    }
}