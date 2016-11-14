namespace Auction.Migrations.Items
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSold : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Sold", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Sold");
        }
    }
}
