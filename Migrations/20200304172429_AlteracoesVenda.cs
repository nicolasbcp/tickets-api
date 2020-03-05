using Microsoft.EntityFrameworkCore.Migrations;

namespace Tickets_API.Migrations
{
    public partial class AlteracoesVenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TotalVenda",
                table: "Vendas",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalVenda",
                table: "Vendas");
        }
    }
}
