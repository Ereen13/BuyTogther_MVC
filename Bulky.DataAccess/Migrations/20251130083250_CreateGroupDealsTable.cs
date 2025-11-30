using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BulkyBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateGroupDealsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.CreateTable(
                name: "GroupDeals",
                columns: table => new
                {
                    DealId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RequiredUsers = table.Column<int>(type: "int", nullable: false),
                    JoinedUsersCount = table.Column<int>(type: "int", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDeals", x => x.DealId);
                    table.ForeignKey(
                        name: "FK_GroupDeals_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupDealUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupDealDealId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDealUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupDealUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupDealUsers_GroupDeals_GroupDealDealId",
                        column: x => x.GroupDealDealId,
                        principalTable: "GroupDeals",
                        principalColumn: "DealId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Praesent vitae sodales libero...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Praesent vitae sodales libero...");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDeals_ProductId",
                table: "GroupDeals",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDealUsers_GroupDealDealId",
                table: "GroupDealUsers",
                column: "GroupDealDealId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDealUsers_UserId",
                table: "GroupDealUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupDealUsers");

            migrationBuilder.DropTable(
                name: "GroupDeals");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 3, "Julian Button", 1, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "RITO5555501", 55.0, 50.0, 35.0, 40.0, "Vanish in the Sunset" },
                    { 4, "Abby Muscles", 2, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "WS3333333301", 70.0, 65.0, 55.0, 60.0, "Cotton Candy" },
                    { 5, "Ron Parker", 2, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "SOTJ1111111101", 30.0, 27.0, 20.0, 25.0, "Rock in the Ocean" },
                    { 6, "Laura Phantom", 3, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "FOT000000001", 25.0, 23.0, 20.0, 22.0, "Leaves and Wonders" }
                });
        }
    }
}
