using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWedding.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "guests",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    mobile = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Message = table.Column<string>(maxLength: 200, nullable: true),
                    IsAttending = table.Column<bool>(nullable: false),
                    dateAdded = table.Column<DateTime>(nullable: false),
                    dateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guests", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guests");
        }
    }
}
