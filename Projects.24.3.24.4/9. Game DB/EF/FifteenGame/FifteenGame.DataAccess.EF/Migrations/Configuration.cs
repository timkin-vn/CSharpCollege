namespace FifteenGame.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FifteenGame.DataAccess.EF.DataContext.FifteenGameDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FifteenGame.DataAccess.EF.DataContext.FifteenGameDataContext context)
        {
           
            var newUser = context.Users.Create();
            newUser.Id = 1;
            newUser.Name = "Admin";
            

            context.Users.AddOrUpdate(u => u.Id, newUser);
            context.SaveChanges();
        }
    }
}
