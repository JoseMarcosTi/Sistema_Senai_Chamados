namespace Andrade.Chamados.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alteração_Tabela_Usuário_03072018 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "TipoUsuario", c => c.Int(nullable: false));
            AddColumn("dbo.Usuarios", "Sexo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "Sexo");
            DropColumn("dbo.Usuarios", "TipoUsuario");
        }
    }
}
