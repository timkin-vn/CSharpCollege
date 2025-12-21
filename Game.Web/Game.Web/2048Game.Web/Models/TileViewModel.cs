using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2048Game.Web.Models
{
    public class TileViewModel
    {
        public TileType Type { get; set; }

        public string CssClass
        {
            get
            {
                switch (Type)
                {
                    case TileType.Wall: return "tile-wall";
                    case TileType.Box: return "tile-box";
                    case TileType.Target: return "tile-target";
                    case TileType.Player: return "tile-player";
                    case TileType.BoxOnTarget: return "tile-box-target";
                    case TileType.PlayerOnTarget: return "tile-player-target";
                    default: return "tile-empty";
                }
            }
        }

        public string Display
        {
            get
            {
                switch (Type)
                {
                    // changed symbols: solid square for box, filled circle for player,
                    // keep a check for box-on-target, use small dot for target
                    case TileType.Box: return "■";
                    case TileType.Player: return "●";
                    case TileType.BoxOnTarget: return "☑";
                    case TileType.PlayerOnTarget: return "●";
                    case TileType.Target: return "·";
                    default: return "";
                }
            }
        }
    }
}