namespace FifteenGame.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWinStreak : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Users", "WinStreak", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("public.Users", "WinStreak");
        }
    }
}
