namespace FifteenGame.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScoreAndIsWin : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Games", "Score", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("public.Games", "IsWin", c => c.Boolean(nullable: false, defaultValue: false));
        }

        public override void Down()
        {
            DropColumn("public.Games", "IsWin");
            DropColumn("public.Games", "Score");
        }
    }
}
