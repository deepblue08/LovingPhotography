namespace PhotoManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                        ClientDate = c.DateTime(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                        Photo_PhotoId = c.Int(),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.Photos", t => t.Photo_PhotoId)
                .Index(t => t.Photo_PhotoId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        PhotoDescription = c.String(),
                        PhotoFileName = c.String(),
                    })
                .PrimaryKey(t => t.PhotoId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Clients", new[] { "Photo_PhotoId" });
            DropForeignKey("dbo.Clients", "Photo_PhotoId", "dbo.Photos");
            DropTable("dbo.Photos");
            DropTable("dbo.Clients");
        }
    }
}
