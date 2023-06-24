using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace EtarChallenge.Data.Migrations
{
    /// <inheritdoc />
    public partial class UsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true, maxLength: 255),
                    username = table.Column<string>(nullable: false, maxLength: 50),
                    password = table.Column<string>(nullable: false, maxLength: 255),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", cols => cols.id);
                    table.UniqueConstraint("UK_users_username", cols => cols.username);
                }
            );


            // Create New Def SuperUser For Testing 
            var passwordHasher = new PasswordHasher<User>();
            var gen_pass = passwordHasher.HashPassword(null, "admin");
            migrationBuilder.Sql($"""
                INSERT INTO users 
                    (name, username, password, createdAt) VALUES
                    ('SuperUser', 'admin', '{gen_pass}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("users");
        }
    }
}
