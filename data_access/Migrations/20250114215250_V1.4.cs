using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data_access.Migrations
{
    /// <inheritdoc />
    public partial class V14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "estado_civil_id",
                table: "Persona",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EstadoCivil",
                columns: table => new
                {
                    estado_civil_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado_descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    estado_activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoCivil", x => x.estado_civil_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persona_estado_civil_id",
                table: "Persona",
                column: "estado_civil_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_EstadoCivil_estado_civil_id",
                table: "Persona",
                column: "estado_civil_id",
                principalTable: "EstadoCivil",
                principalColumn: "estado_civil_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persona_EstadoCivil_estado_civil_id",
                table: "Persona");

            migrationBuilder.DropTable(
                name: "EstadoCivil");

            migrationBuilder.DropIndex(
                name: "IX_Persona_estado_civil_id",
                table: "Persona");

            migrationBuilder.DropColumn(
                name: "estado_civil_id",
                table: "Persona");
        }
    }
}
