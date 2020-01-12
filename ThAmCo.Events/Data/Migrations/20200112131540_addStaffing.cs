using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Events.Data.Migrations
{
    public partial class addStaffing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                schema: "thamco.events",
                table: "Staff",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_EventId",
                schema: "thamco.events",
                table: "Staff",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Events_EventId",
                schema: "thamco.events",
                table: "Staff",
                column: "EventId",
                principalSchema: "thamco.events",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Events_EventId",
                schema: "thamco.events",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_EventId",
                schema: "thamco.events",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "thamco.events",
                table: "Staff");
        }
    }
}
