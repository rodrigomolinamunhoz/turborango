namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Avaliacaos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avaliacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Media = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        IdRestaurante = c.Int(nullable: false),
                        Restaurante_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurantes", t => t.Restaurante_Id)
                .Index(t => t.Restaurante_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Avaliacaos", "Restaurante_Id", "dbo.Restaurantes");
            DropIndex("dbo.Avaliacaos", new[] { "Restaurante_Id" });
            DropTable("dbo.Avaliacaos");
        }
    }
}
