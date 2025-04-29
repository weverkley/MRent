using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringIdentifiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_motolog_identificador",
                table: "motolog");

            migrationBuilder.DropIndex(
                name: "IX_moto_identificador",
                table: "moto");

            migrationBuilder.DropIndex(
                name: "IX_entregador_identificador",
                table: "entregador");

            migrationBuilder.DropColumn(
                name: "identificador",
                table: "motolog");

            migrationBuilder.DropColumn(
                name: "identificador",
                table: "moto");

            migrationBuilder.DropColumn(
                name: "identificador",
                table: "entregador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "identificador",
                table: "motolog",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "identificador",
                table: "moto",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "identificador",
                table: "entregador",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_motolog_identificador",
                table: "motolog",
                column: "identificador");

            migrationBuilder.CreateIndex(
                name: "IX_moto_identificador",
                table: "moto",
                column: "identificador");

            migrationBuilder.CreateIndex(
                name: "IX_entregador_identificador",
                table: "entregador",
                column: "identificador");
        }
    }
}
