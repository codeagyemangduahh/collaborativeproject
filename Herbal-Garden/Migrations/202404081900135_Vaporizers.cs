namespace Herbal_Garden.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vaporizers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gardens",
                c => new
                    {
                        GardenID = c.Int(nullable: false, identity: true),
                        GardenName = c.String(),
                        SoilType = c.String(),
                    })
                .PrimaryKey(t => t.GardenID);
            
            CreateTable(
                "dbo.Herbs",
                c => new
                    {
                        HerbsID = c.Int(nullable: false, identity: true),
                        HerbsName = c.String(),
                        Treatment = c.String(),
                        GardenID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HerbsID)
                .ForeignKey("dbo.Gardens", t => t.GardenID, cascadeDelete: true)
                .Index(t => t.GardenID);
            
            CreateTable(
                "dbo.Vaporizers",
                c => new
                    {
                        VaporizerID = c.Int(nullable: false, identity: true),
                        VaporizerName = c.String(),
                        SupplierID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VaporizerID)
                .ForeignKey("dbo.Suppliers", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(),
                    })
                .PrimaryKey(t => t.SupplierID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.VaporizerHerbs",
                c => new
                    {
                        Vaporizer_VaporizerID = c.Int(nullable: false),
                        Herbs_HerbsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vaporizer_VaporizerID, t.Herbs_HerbsID })
                .ForeignKey("dbo.Vaporizers", t => t.Vaporizer_VaporizerID, cascadeDelete: true)
                .ForeignKey("dbo.Herbs", t => t.Herbs_HerbsID, cascadeDelete: true)
                .Index(t => t.Vaporizer_VaporizerID)
                .Index(t => t.Herbs_HerbsID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Vaporizers", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.VaporizerHerbs", "Herbs_HerbsID", "dbo.Herbs");
            DropForeignKey("dbo.VaporizerHerbs", "Vaporizer_VaporizerID", "dbo.Vaporizers");
            DropForeignKey("dbo.Herbs", "GardenID", "dbo.Gardens");
            DropIndex("dbo.VaporizerHerbs", new[] { "Herbs_HerbsID" });
            DropIndex("dbo.VaporizerHerbs", new[] { "Vaporizer_VaporizerID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Vaporizers", new[] { "SupplierID" });
            DropIndex("dbo.Herbs", new[] { "GardenID" });
            DropTable("dbo.VaporizerHerbs");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Vaporizers");
            DropTable("dbo.Herbs");
            DropTable("dbo.Gardens");
        }
    }
}
