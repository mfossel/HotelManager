namespace HotelManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userupdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reservations", "TotalCost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "TotalCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
