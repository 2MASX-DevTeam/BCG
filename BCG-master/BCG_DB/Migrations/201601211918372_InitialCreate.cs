namespace BCG_DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblIPLoginAttempts",
                c => new
                    {
                        IdIPAdress = c.Int(nullable: false, identity: true),
                        IPAdress = c.String(maxLength: 50),
                        UserAgend = c.String(maxLength: 500),
                        DateChanged = c.DateTime(),
                        DateCreated = c.DateTime(),
                        UserName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdIPAdress);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblIPLoginAttempts");
        }
    }
}
