using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiClienteXp.Migrations
{
    /// <inheritdoc />
    public partial class PopulaClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Clientes(Nome, cpf, email) values('Joao','11111111111','joao@gmail.com')");
            mb.Sql("Insert into Clientes(Nome, cpf, email) values('Maria','22222222222','maria@gmail.com')");
            mb.Sql("Insert into Clientes(Nome, cpf, email) values('Gustavo','33333333333','gustavo@gmail.com')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Clientes");
        }
    }
}
