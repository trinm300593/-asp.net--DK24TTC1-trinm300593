using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BubbleTeaCafe.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CustomerEmail = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CustomerPhone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DeliveryAddress = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    OrderType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    IsAvailable = table.Column<bool>(type: "INTEGER", nullable: false),
                    Ingredients = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    SizeOptions = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    TemperatureOptions = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    SweetnessOptions = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Size = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Temperature = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Sweetness = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    SpecialInstructions = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Bubble Tea với nhiều hương vị thơm ngon", "Trà Sữa" },
                    { 2, "Cà phê rang xay thơm ngon", "Cà Phê" },
                    { 3, "Trà trái cây tươi mát", "Trà Trái Cây" },
                    { 4, "Sinh tố trái cây tươi ngon", "Smoothie" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "ImageUrl", "Ingredients", "IsAvailable", "Name", "Price", "SizeOptions", "SweetnessOptions", "TemperatureOptions" },
                values: new object[,]
                {
                    { 1, 1, "Trà sữa đậm đà với trân châu đen", "/images/tra-sua-truyen-thong.svg", "Trà đen, sữa tươi, trân châu đen", true, "Trà Sữa Truyền Thống", 35000m, "M,L", "0%,25%,50%,75%,100%", "Cold,Hot" },
                    { 2, 1, "Trà sữa matcha Nhật Bản thơm ngon", "/images/tra-sua-matcha.svg", "Matcha Nhật Bản, sữa tươi, trân châu trắng", true, "Trà Sữa Matcha", 40000m, "M,L", "0%,25%,50%,75%,100%", "Cold,Hot" },
                    { 3, 1, "Trà sữa khoai môn tím thơm béo", "https://images.unsplash.com/photo-1571934811356-5cc061b6821f?w=400&h=400&fit=crop&crop=center", "Khoai môn tím, sữa tươi, trân châu đen", true, "Trà Sữa Taro", 38000m, "M,L", "0%,25%,50%,75%,100%", "Cold,Hot" },
                    { 4, 2, "Cà phê đen truyền thống Việt Nam", "/images/ca-phe-den-da.svg", "Cà phê robusta rang xay", true, "Cà Phê Đen Đá", 25000m, "M,L", "0%,25%,50%,75%,100%", "Cold,Hot" },
                    { 5, 2, "Cà phê sữa đá đậm đà", "/images/ca-phe-sua-da.svg", "Cà phê robusta, sữa đặc", true, "Cà Phê Sữa Đá", 30000m, "M,L", "0%,25%,50%,75%,100%", "Cold,Hot" },
                    { 6, 2, "Cappuccino Ý thơm ngon", "https://images.unsplash.com/photo-1534778101976-62847782c213?w=400&h=400&fit=crop&crop=center", "Espresso, sữa tươi, bọt sữa", true, "Cappuccino", 45000m, "M,L", "0%,25%,50%,75%,100%", "Hot" },
                    { 7, 3, "Trà đào tươi mát", "/images/tra-dao.svg", "Trà xanh, đào tươi, mật ong", true, "Trà Đào", 32000m, "M,L", "0%,25%,50%,75%,100%", "Cold,Iced" },
                    { 8, 3, "Trà chanh chua ngọt", "https://images.unsplash.com/photo-1556679343-c7306c1976bc?w=400&h=400&fit=crop&crop=center", "Trà đen, chanh tươi, đường", true, "Trà Chanh", 28000m, "M,L", "0%,25%,50%,75%,100%", "Cold,Iced" },
                    { 9, 4, "Sinh tố xoài tươi ngon", "/images/sinh-to-xoai.svg", "Xoài tươi, sữa tươi, đá", true, "Sinh Tố Xoài", 42000m, "M,L", "0%,25%,50%,75%,100%", "Cold" },
                    { 10, 4, "Sinh tố dâu tây tươi mát", "https://images.unsplash.com/photo-1553530666-ba11a7da3888?w=400&h=400&fit=crop&crop=center", "Dâu tây tươi, sữa tươi, đá", true, "Sinh Tố Dâu", 45000m, "M,L", "0%,25%,50%,75%,100%", "Cold" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
