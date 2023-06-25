using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EtarChallenge.Data.Migrations
{
    /// <inheritdoc />
    public partial class ItemsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("items",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false, maxLength: 255),
                    price = table.Column<float>(nullable: false),
                    description = table.Column<string>(nullable: false, maxLength: 500),
                    catId = table.Column<int>(nullable: false),
                    createdBy = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", cols => cols.id);
                    table.ForeignKey("FK_items_users_createdBy", cols => cols.createdBy, "users");
                    table.ForeignKey("FK_items_categories_catId", cols => cols.catId, "categories");
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("items");
        }

    }
}
