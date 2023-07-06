using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopTicketCinema.Web.Data.Migrations
{
    public partial class datemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateValid",
                table: "Tickets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateValid",
                table: "Tickets");
        }
    }
}
