using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMotorcycleLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "motolog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    moto_id = table.Column<Guid>(type: "uuid", nullable: false),
                    identificador = table.Column<string>(type: "text", nullable: false),
                    ano = table.Column<int>(type: "integer", nullable: false),
                    modelo = table.Column<string>(type: "text", nullable: false),
                    placa = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motolog", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_motolog_identificador",
                table: "motolog",
                column: "identificador");

            migrationBuilder.CreateIndex(
                name: "IX_motolog_placa",
                table: "motolog",
                column: "placa",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "motolog");
        }
    }
}
