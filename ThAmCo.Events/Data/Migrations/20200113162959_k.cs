using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Events.Data.Migrations
{
    public partial class k : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddStaffModel",
                schema: "thamco.events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: true),
                    TypeId = table.Column<string>(maxLength: 3, nullable: false),
                    StaffingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddStaffModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddStaffModel_Staff_StaffingId",
                        column: x => x.StaffingId,
                        principalSchema: "thamco.events",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddStaffModel_StaffingId",
                schema: "thamco.events",
                table: "AddStaffModel",
                column: "StaffingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddStaffModel",
                schema: "thamco.events");
        }
    }
}
