using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRelacionEscuelaAlumno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumnos_Escuelas_EscuelaId",
                table: "Alumnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Profesores_Escuelas_EscuelaId",
                table: "Profesores");

            migrationBuilder.DropColumn(
                name: "IdEscuela",
                table: "Profesores");

            migrationBuilder.DropColumn(
                name: "IdEscuela",
                table: "Alumnos");

            migrationBuilder.AlterColumn<int>(
                name: "EscuelaId",
                table: "Profesores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EscuelaId",
                table: "Alumnos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Alumnos_Escuelas_EscuelaId",
                table: "Alumnos",
                column: "EscuelaId",
                principalTable: "Escuelas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profesores_Escuelas_EscuelaId",
                table: "Profesores",
                column: "EscuelaId",
                principalTable: "Escuelas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumnos_Escuelas_EscuelaId",
                table: "Alumnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Profesores_Escuelas_EscuelaId",
                table: "Profesores");

            migrationBuilder.AlterColumn<int>(
                name: "EscuelaId",
                table: "Profesores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdEscuela",
                table: "Profesores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "EscuelaId",
                table: "Alumnos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdEscuela",
                table: "Alumnos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Alumnos_Escuelas_EscuelaId",
                table: "Alumnos",
                column: "EscuelaId",
                principalTable: "Escuelas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesores_Escuelas_EscuelaId",
                table: "Profesores",
                column: "EscuelaId",
                principalTable: "Escuelas",
                principalColumn: "Id");
        }
    }
}
