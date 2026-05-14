namespace Minesweeper.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Minesweeper.DataAccess.EF.Data.MinesweeperDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Minesweeper.DataAccess.EF.Data.MinesweeperDbContext context)
        {
        }
    }
}
