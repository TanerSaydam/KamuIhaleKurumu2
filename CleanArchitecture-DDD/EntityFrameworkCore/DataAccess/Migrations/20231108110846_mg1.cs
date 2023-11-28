using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillingDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CartType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleMainRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MainRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMainRoles", x => new { x.MainRoleId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Category 0" },
                    { 2, "Category 1" },
                    { 3, "Category 2" },
                    { 4, "Category 3" },
                    { 5, "Category 4" },
                    { 6, "Category 5" },
                    { 7, "Category 6" },
                    { 8, "Category 7" },
                    { 9, "Category 8" },
                    { 10, "Category 9" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 6, "Product 0", 0m },
                    { 2, 7, "Product 1", 5m },
                    { 3, 5, "Product 2", 10m },
                    { 4, 6, "Product 3", 15m },
                    { 5, 1, "Product 4", 20m },
                    { 6, 6, "Product 5", 25m },
                    { 7, 2, "Product 6", 30m },
                    { 8, 9, "Product 7", 35m },
                    { 9, 8, "Product 8", 40m },
                    { 10, 1, "Product 9", 45m },
                    { 11, 8, "Product 10", 50m },
                    { 12, 4, "Product 11", 55m },
                    { 13, 4, "Product 12", 60m },
                    { 14, 1, "Product 13", 65m },
                    { 15, 3, "Product 14", 70m },
                    { 16, 6, "Product 15", 75m },
                    { 17, 6, "Product 16", 80m },
                    { 18, 3, "Product 17", 85m },
                    { 19, 4, "Product 18", 90m },
                    { 20, 1, "Product 19", 95m },
                    { 21, 8, "Product 20", 100m },
                    { 22, 1, "Product 21", 105m },
                    { 23, 5, "Product 22", 110m },
                    { 24, 7, "Product 23", 115m },
                    { 25, 6, "Product 24", 120m },
                    { 26, 3, "Product 25", 125m },
                    { 27, 2, "Product 26", 130m },
                    { 28, 1, "Product 27", 135m },
                    { 29, 9, "Product 28", 140m },
                    { 30, 2, "Product 29", 145m },
                    { 31, 2, "Product 30", 150m },
                    { 32, 4, "Product 31", 155m },
                    { 33, 5, "Product 32", 160m },
                    { 34, 6, "Product 33", 165m },
                    { 35, 4, "Product 34", 170m },
                    { 36, 4, "Product 35", 175m },
                    { 37, 9, "Product 36", 180m },
                    { 38, 9, "Product 37", 185m },
                    { 39, 2, "Product 38", 190m },
                    { 40, 7, "Product 39", 195m },
                    { 41, 4, "Product 40", 200m },
                    { 42, 6, "Product 41", 205m },
                    { 43, 1, "Product 42", 210m },
                    { 44, 9, "Product 43", 215m },
                    { 45, 6, "Product 44", 220m },
                    { 46, 5, "Product 45", 225m },
                    { 47, 2, "Product 46", 230m },
                    { 48, 5, "Product 47", 235m },
                    { 49, 3, "Product 48", 240m },
                    { 50, 5, "Product 49", 245m },
                    { 51, 6, "Product 50", 250m },
                    { 52, 7, "Product 51", 255m },
                    { 53, 1, "Product 52", 260m },
                    { 54, 2, "Product 53", 265m },
                    { 55, 3, "Product 54", 270m },
                    { 56, 2, "Product 55", 275m },
                    { 57, 4, "Product 56", 280m },
                    { 58, 6, "Product 57", 285m },
                    { 59, 7, "Product 58", 290m },
                    { 60, 2, "Product 59", 295m },
                    { 61, 2, "Product 60", 300m },
                    { 62, 3, "Product 61", 305m },
                    { 63, 6, "Product 62", 310m },
                    { 64, 1, "Product 63", 315m },
                    { 65, 5, "Product 64", 320m },
                    { 66, 4, "Product 65", 325m },
                    { 67, 7, "Product 66", 330m },
                    { 68, 1, "Product 67", 335m },
                    { 69, 5, "Product 68", 340m },
                    { 70, 3, "Product 69", 345m },
                    { 71, 5, "Product 70", 350m },
                    { 72, 3, "Product 71", 355m },
                    { 73, 6, "Product 72", 360m },
                    { 74, 1, "Product 73", 365m },
                    { 75, 6, "Product 74", 370m },
                    { 76, 2, "Product 75", 375m },
                    { 77, 7, "Product 76", 380m },
                    { 78, 7, "Product 77", 385m },
                    { 79, 7, "Product 78", 390m },
                    { 80, 9, "Product 79", 395m },
                    { 81, 9, "Product 80", 400m },
                    { 82, 3, "Product 81", 405m },
                    { 83, 6, "Product 82", 410m },
                    { 84, 5, "Product 83", 415m },
                    { 85, 6, "Product 84", 420m },
                    { 86, 8, "Product 85", 425m },
                    { 87, 4, "Product 86", 430m },
                    { 88, 4, "Product 87", 435m },
                    { 89, 5, "Product 88", 440m },
                    { 90, 6, "Product 89", 445m },
                    { 91, 8, "Product 90", 450m },
                    { 92, 3, "Product 91", 455m },
                    { 93, 4, "Product 92", 460m },
                    { 94, 2, "Product 93", 465m },
                    { 95, 3, "Product 94", 470m },
                    { 96, 3, "Product 95", 475m },
                    { 97, 8, "Product 96", 480m },
                    { 98, 7, "Product 97", 485m },
                    { 99, 5, "Product 98", 490m },
                    { 100, 4, "Product 99", 495m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name_Price",
                table: "Products",
                columns: new[] { "Name", "Price" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingDetail");

            migrationBuilder.DropTable(
                name: "MainRoles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RoleMainRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
