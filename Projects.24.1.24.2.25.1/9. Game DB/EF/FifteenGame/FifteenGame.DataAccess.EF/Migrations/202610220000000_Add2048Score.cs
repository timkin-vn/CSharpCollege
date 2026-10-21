using System.Data.Entity.Migrations;

namespace FifteenGame.DataAccess.EF.Migrations
{
    public partial class Add2048Score : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Games", "Score", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("public.Games", "BestScore", c => c.Int(nullable: false, defaultValue: 0));
        }

        public override void Down()
        {
            DropColumn("public.Games", "BestScore");
            DropColumn("public.Games", "Score");
        }
    }
}