using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantOrderApp.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderOptions",
                columns: table => new
                {
                    DishType = table.Column<int>(nullable: false),
                    TimeOfDay = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    MaxQuantity = table.Column<int>(nullable: false),
                    MinQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderOptions", x => new { x.DishType, x.TimeOfDay });
                });

            migrationBuilder.InsertData(
                table: "OrderOptions",
                columns: new[] { "DishType", "TimeOfDay", "MaxQuantity", "MinQuantity", "Name" },
                values: new object[,]
                {
                    { 1, 0, 1, 1, "eggs" },
                    { 2, 0, 1, 1, "toast" },
                    { 3, 0, 100, 1, "coffee" },
                    { 1, 1, 1, 1, "steak" },
                    { 2, 1, 100, 1, "potato" },
                    { 3, 1, 1, 1, "wine" },
                    { 4, 1, 1, 1, "cake" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderOptions");
        }
    }
}
