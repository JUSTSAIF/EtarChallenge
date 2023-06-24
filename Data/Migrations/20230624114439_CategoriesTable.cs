using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EtarChallenge.Data.Migrations
{
    /// <inheritdoc />
    public partial class CategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("categories",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false, maxLength: 255),
                    description = table.Column<string>(nullable: false, maxLength: 500),
                    createdAt = table.Column<DateTime>(nullable: false),
                    createdBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", cols => cols.id);
                    table.ForeignKey("FK_categories_users_createdBy", cols => cols.createdBy, "users");
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("categories");
        }
    }
}
