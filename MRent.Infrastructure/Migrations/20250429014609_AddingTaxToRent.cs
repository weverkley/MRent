using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingTaxToRent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "data_devolucao",
                table: "locacao",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "subtotal",
                table: "locacao",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "taxas",
                table: "locacao",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "total",
                table: "locacao",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "plano",
                columns: new[] { "id", "taxa_dia_excedido", "valor_diaria", "dias", "porcentagem_taxa_retorno" },
                values: new object[,]
                {
                    { new Guid("466d0330-70b4-47b5-ae99-d3f62d40bd20"), 50, 22.0, 30, 0 },
                    { new Guid("6307d574-0979-4f1c-8761-a3425b4c955c"), 50, 28.0, 15, 40 },
                    { new Guid("88924edf-91f6-4130-b54a-b51dc796da93"), 50, 20.0, 45, 0 },
                    { new Guid("96234321-504d-47a3-ad27-f20ec91c9036"), 50, 30.0, 7, 20 },
                    { new Guid("d0840305-d467-44ac-ac24-e4e791a58ed3"), 50, 18.0, 50, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "plano",
                keyColumn: "id",
                keyValue: new Guid("466d0330-70b4-47b5-ae99-d3f62d40bd20"));

            migrationBuilder.DeleteData(
                table: "plano",
                keyColumn: "id",
                keyValue: new Guid("6307d574-0979-4f1c-8761-a3425b4c955c"));

            migrationBuilder.DeleteData(
                table: "plano",
                keyColumn: "id",
                keyValue: new Guid("88924edf-91f6-4130-b54a-b51dc796da93"));

            migrationBuilder.DeleteData(
                table: "plano",
                keyColumn: "id",
                keyValue: new Guid("96234321-504d-47a3-ad27-f20ec91c9036"));

            migrationBuilder.DeleteData(
                table: "plano",
                keyColumn: "id",
                keyValue: new Guid("d0840305-d467-44ac-ac24-e4e791a58ed3"));

            migrationBuilder.DropColumn(
                name: "data_devolucao",
                table: "locacao");

            migrationBuilder.DropColumn(
                name: "subtotal",
                table: "locacao");

            migrationBuilder.DropColumn(
                name: "taxas",
                table: "locacao");

            migrationBuilder.DropColumn(
                name: "total",
                table: "locacao");
        }
    }
}
