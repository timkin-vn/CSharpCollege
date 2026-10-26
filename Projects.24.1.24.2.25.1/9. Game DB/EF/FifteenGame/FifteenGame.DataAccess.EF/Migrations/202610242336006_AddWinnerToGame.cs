namespace FifteenGame.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWinnerToGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Games", "Winner", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("public.Games", "Winner");
        }
    }
}
