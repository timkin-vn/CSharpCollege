namespace FifteenGame.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Cells", "PeopleCount", c => c.Int(nullable: false));
            AddColumn("public.Cells", "HasShop", c => c.Boolean(nullable: false));
            AddColumn("public.Cells", "IsVeggie", c => c.Boolean(nullable: false));
            AddColumn("public.Cells", "IsRevealed", c => c.Boolean(nullable: false));
            AddColumn("public.Games", "Money", c => c.Int(nullable: false));
            AddColumn("public.Games", "TurnsPlayed", c => c.Int(nullable: false));
            DropColumn("public.Cells", "Value");
            DropColumn("public.Games", "MoveCount");
        }
        
        public override void Down()
        {
            AddColumn("public.Games", "MoveCount", c => c.Int(nullable: false));
            AddColumn("public.Cells", "Value", c => c.Int(nullable: false));
            DropColumn("public.Games", "TurnsPlayed");
            DropColumn("public.Games", "Money");
            DropColumn("public.Cells", "IsRevealed");
            DropColumn("public.Cells", "IsVeggie");
            DropColumn("public.Cells", "HasShop");
            DropColumn("public.Cells", "PeopleCount");
        }
    }
}
