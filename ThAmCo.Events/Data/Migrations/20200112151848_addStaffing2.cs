using Microsoft.EntityFrameworkCore.Migrations;
using System;
using ThAmCo.Events.Models;
namespace ThAmCo.Events.Data.Migrations
{
    public partial class addStaffing2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(

              schema: "thamco.events",
              table: "Staff",
              columns: new[] { "Id", "Name", "FirstAider" },
              values: new object[,]
              {
                    {1,"danny", true }
              }
              );
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
