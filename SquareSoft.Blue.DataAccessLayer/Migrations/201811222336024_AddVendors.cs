namespace SquareSoft.Blue.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVendors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.vendors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VendorCopName = c.String(),
                        HQAddress = c.String(),
                        Tel = c.String(),
                        HeadPersonName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastEdited = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.vendors");
        }
    }
}
