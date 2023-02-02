using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "user1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "user2" });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "Id", "Amount", "Name", "Status", "UserId" },
                values: new object[] { 1, 12000, "Contract1", 1, 1 });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "Id", "Amount", "Name", "Status", "UserId" },
                values: new object[] { 2, 24000, "Contract2", 1, 1 });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "Id", "Amount", "Name", "Status", "UserId" },
                values: new object[] { 3, 34000, "Contract2", 1, 2 });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "Id", "Amount", "Name", "Status", "UserId" },
                values: new object[] { 4, 14000, "Contract2", 0, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_UserId",
                table: "Contracts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
