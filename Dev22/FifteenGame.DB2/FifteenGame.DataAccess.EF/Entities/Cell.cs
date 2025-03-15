﻿using FifteenGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Entities
{
    public class Cell
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public CellsModel Value { get; set; }

        [Required]
        public Game Game { get; set; }
    }
}
