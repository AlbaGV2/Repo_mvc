using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiCafeteria.Data.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class AddFechas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DechaCreacion",
                table: "Categorias",
                newName: "FechaCreacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Categorias",
                newName: "DechaCreacion");
        }
    }
}
