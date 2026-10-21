namespace MemoryGame.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CellStateAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Cells", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("public.Cells", "State");
        }
    }
}
