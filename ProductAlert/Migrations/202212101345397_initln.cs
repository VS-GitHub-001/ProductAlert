namespace ProductAlert.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initln : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Agency_Id", "dbo.Agencies");
            DropForeignKey("dbo.Products", "Manufacturer_Id", "dbo.Manufacturers");
            DropIndex("dbo.Products", new[] { "Agency_Id" });
            DropIndex("dbo.Products", new[] { "Manufacturer_Id" });
            RenameColumn(table: "dbo.Products", name: "Agency_Id", newName: "AgencyId");
            RenameColumn(table: "dbo.Products", name: "Manufacturer_Id", newName: "ManufacturerId");
            AlterColumn("dbo.Products", "AgencyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "ManufacturerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "AgencyId");
            CreateIndex("dbo.Products", "ManufacturerId");
            AddForeignKey("dbo.Products", "AgencyId", "dbo.Agencies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Products", "AgencyId", "dbo.Agencies");
            DropIndex("dbo.Products", new[] { "ManufacturerId" });
            DropIndex("dbo.Products", new[] { "AgencyId" });
            AlterColumn("dbo.Products", "ManufacturerId", c => c.Int());
            AlterColumn("dbo.Products", "AgencyId", c => c.Int());
            RenameColumn(table: "dbo.Products", name: "ManufacturerId", newName: "Manufacturer_Id");
            RenameColumn(table: "dbo.Products", name: "AgencyId", newName: "Agency_Id");
            CreateIndex("dbo.Products", "Manufacturer_Id");
            CreateIndex("dbo.Products", "Agency_Id");
            AddForeignKey("dbo.Products", "Manufacturer_Id", "dbo.Manufacturers", "Id");
            AddForeignKey("dbo.Products", "Agency_Id", "dbo.Agencies", "Id");
        }
    }
}
