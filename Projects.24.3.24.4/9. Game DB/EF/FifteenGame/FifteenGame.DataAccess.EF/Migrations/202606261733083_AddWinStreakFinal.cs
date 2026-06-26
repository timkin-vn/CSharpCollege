namespace FifteenGame.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWinStreakFinal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("public.Users", "WinStreak", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            AlterColumn("public.Users", "WinStreak", c => c.Int());
        }
    }
}
