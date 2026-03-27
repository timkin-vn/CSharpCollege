namespace FifteenGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSavedGameField : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.users", "SavedGameJson", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("public.users", "SavedGameJson");
        }
    }
}
