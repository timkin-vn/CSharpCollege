namespace PacmanGame.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PacmanGame.DataAccess.PacmanDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PacmanGame.DataAccess.PacmanDbContext context)
        {
            
        }
    }
}
