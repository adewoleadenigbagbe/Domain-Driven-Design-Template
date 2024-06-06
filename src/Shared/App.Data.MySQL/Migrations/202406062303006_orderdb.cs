namespace App.Data.MySQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AddressLine = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        CustomerId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeprecated = c.Boolean(nullable: false),
                        Customer_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.CreatedOn)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Dob = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeprecated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orderlines",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Guid(nullable: false),
                        IsDeprecated = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Order_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.CreatedOn)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeprecated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CustomerId)
                .Index(t => t.CreatedOn);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeprecated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orderlines", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orderlines", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Addresses", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Addresses", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CreatedOn" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Orderlines", new[] { "Order_Id" });
            DropIndex("dbo.Orderlines", new[] { "CreatedOn" });
            DropIndex("dbo.Orderlines", new[] { "OrderId" });
            DropIndex("dbo.Orderlines", new[] { "ProductId" });
            DropIndex("dbo.Addresses", new[] { "Customer_Id" });
            DropIndex("dbo.Addresses", new[] { "CreatedOn" });
            DropIndex("dbo.Addresses", new[] { "CustomerId" });
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.Orderlines");
            DropTable("dbo.Customers");
            DropTable("dbo.Addresses");
        }
    }
}
