namespace MVCHoldem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddingPostModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 100),
                        Content = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Author_Id);
        }
        
        public override void Down()
        {
            this.DropForeignKey("dbo.Posts", "Author_Id", "dbo.Users");
            this.DropIndex("dbo.Posts", new[] { "Author_Id" });
            this.DropIndex("dbo.Posts", new[] { "IsDeleted" });
            this.DropTable("dbo.Posts");
        }
    }
}
