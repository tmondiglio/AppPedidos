using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class AgregadoFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdCliente",
                table: "Pedidos",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItems_Pedidos_IdPedido",
                table: "PedidoItems",
                column: "IdPedido",
                principalTable: "Pedidos",
                principalColumn: "IdPedido",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_IdCliente",
                table: "Pedidos",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItems_Pedidos_IdPedido",
                table: "PedidoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_IdCliente",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_IdCliente",
                table: "Pedidos");
        }
    }
}
