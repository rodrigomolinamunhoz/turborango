namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Avaliacao_Fk_Restaurante : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Avaliacaos", "IdRestaurante");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Avaliacaos", "IdRestaurante", c => c.Int(nullable: false));
        }
    }
}
