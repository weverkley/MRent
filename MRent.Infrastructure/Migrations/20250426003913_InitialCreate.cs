using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entregador",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    identificador = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    cnpj = table.Column<string>(type: "text", nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    numero_cnh = table.Column<string>(type: "text", nullable: false),
                    tipo_cnh = table.Column<int>(type: "integer", nullable: false),
                    imagem_cnh = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entregador", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "moto",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    identificador = table.Column<string>(type: "text", nullable: false),
                    ano = table.Column<int>(type: "integer", nullable: false),
                    modelo = table.Column<string>(type: "text", nullable: false),
                    placa = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "plano",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    dias = table.Column<int>(type: "integer", nullable: false),
                    valor_diaria = table.Column<double>(type: "double precision", nullable: false),
                    porcentagem_taxa_retorno = table.Column<int>(type: "integer", nullable: false),
                    taxa_dia_excedido = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plano", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "locacao",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    entregador_id = table.Column<Guid>(type: "uuid", nullable: false),
                    moto_id = table.Column<Guid>(type: "uuid", nullable: false),
                    plano_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_termino = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_previsao_termino = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locacao", x => x.id);
                    table.ForeignKey(
                        name: "FK_locacao_entregador_entregador_id",
                        column: x => x.entregador_id,
                        principalTable: "entregador",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_locacao_moto_moto_id",
                        column: x => x.moto_id,
                        principalTable: "moto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_locacao_plano_plano_id",
                        column: x => x.plano_id,
                        principalTable: "plano",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entregador_cnpj",
                table: "entregador",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_entregador_identificador",
                table: "entregador",
                column: "identificador");

            migrationBuilder.CreateIndex(
                name: "IX_entregador_numero_cnh",
                table: "entregador",
                column: "numero_cnh",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_locacao_entregador_id",
                table: "locacao",
                column: "entregador_id");

            migrationBuilder.CreateIndex(
                name: "IX_locacao_moto_id",
                table: "locacao",
                column: "moto_id");

            migrationBuilder.CreateIndex(
                name: "IX_locacao_plano_id",
                table: "locacao",
                column: "plano_id");

            migrationBuilder.CreateIndex(
                name: "IX_moto_identificador",
                table: "moto",
                column: "identificador");

            migrationBuilder.CreateIndex(
                name: "IX_moto_placa",
                table: "moto",
                column: "placa",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "locacao");

            migrationBuilder.DropTable(
                name: "entregador");

            migrationBuilder.DropTable(
                name: "moto");

            migrationBuilder.DropTable(
                name: "plano");
        }
    }
}
