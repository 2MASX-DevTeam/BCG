namespace BCG_DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblContext",
                c => new
                    {
                        IdContext = c.Int(nullable: false, identity: true),
                        Context = c.String(nullable: false, maxLength: 500),
                        UserName = c.String(maxLength: 50),
                        DateChanged = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.IdContext);
            
            CreateTable(
                "dbo.tblResources",
                c => new
                    {
                        IdResource = c.Int(nullable: false),
                        IdLanguage = c.Int(nullable: false),
                        IdContext = c.Int(nullable: false),
                        UserName = c.String(maxLength: 50),
                        DateChanged = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.IdResource, t.IdLanguage, t.IdContext })
                .ForeignKey("dbo.tblLanguages", t => t.IdLanguage)
                .ForeignKey("dbo.tblContext", t => t.IdContext)
                .Index(t => t.IdLanguage)
                .Index(t => t.IdContext);
            
            CreateTable(
                "dbo.tblLanguages",
                c => new
                    {
                        IdLanguage = c.Int(nullable: false, identity: true),
                        Language = c.String(nullable: false, maxLength: 50),
                        Initials = c.String(nullable: false, maxLength: 10),
                        Culture = c.String(nullable: false, maxLength: 20),
                        Picture = c.Binary(storeType: "image"),
                        IsActive = c.Boolean(nullable: false),
                        UserName = c.String(maxLength: 50),
                        Datechanged = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.IdLanguage);
            
            CreateTable(
                "dbo.tblStaticResources",
                c => new
                    {
                        IdStatic = c.Int(nullable: false, identity: true),
                        IdLanguage = c.Int(nullable: false),
                        IdStaticText = c.Int(nullable: false),
                        Description = c.String(maxLength: 100),
                        StaticName = c.String(maxLength: 100),
                        UserName = c.String(maxLength: 50),
                        DateChanged = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.IdStatic, t.IdLanguage, t.IdStaticText })
                .ForeignKey("dbo.tblStaticTexts", t => t.IdStaticText)
                .ForeignKey("dbo.tblLanguages", t => t.IdLanguage)
                .Index(t => t.IdLanguage)
                .Index(t => t.IdStaticText);
            
            CreateTable(
                "dbo.tblStaticTexts",
                c => new
                    {
                        IdStaticText = c.Int(nullable: false, identity: true),
                        StaticText = c.String(nullable: false, unicode: false, storeType: "text"),
                        UserName = c.String(maxLength: 50),
                        DateChanged = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.IdStaticText);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblResources", "IdContext", "dbo.tblContext");
            DropForeignKey("dbo.tblStaticResources", "IdLanguage", "dbo.tblLanguages");
            DropForeignKey("dbo.tblStaticResources", "IdStaticText", "dbo.tblStaticTexts");
            DropForeignKey("dbo.tblResources", "IdLanguage", "dbo.tblLanguages");
            DropIndex("dbo.tblStaticResources", new[] { "IdStaticText" });
            DropIndex("dbo.tblStaticResources", new[] { "IdLanguage" });
            DropIndex("dbo.tblResources", new[] { "IdContext" });
            DropIndex("dbo.tblResources", new[] { "IdLanguage" });
            DropTable("dbo.tblStaticTexts");
            DropTable("dbo.tblStaticResources");
            DropTable("dbo.tblLanguages");
            DropTable("dbo.tblResources");
            DropTable("dbo.tblContext");
        }
    }
}
