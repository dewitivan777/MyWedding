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
                    name = table.Column<string>(nullable: false),
                    surname = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    mobile = table.Column<string>(nullable: true),
                    isattending = table.Column<bool>(nullable: false),
                    date = table.Column<DateTime>(nullable: false)
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
