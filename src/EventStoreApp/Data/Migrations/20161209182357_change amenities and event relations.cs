using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventStoreApp.Data.Migrations
{
    public partial class changeamenitiesandeventrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventAmenity");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Amenities",
                newName: "AmenityId");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Amenities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_EventId",
                table: "Amenities",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Events_EventId",
                table: "Amenities",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Events_EventId",
                table: "Amenities");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_EventId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Amenities");

            migrationBuilder.RenameColumn(
                name: "AmenityId",
                table: "Amenities",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "EventAmenity",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false),
                    AmenityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAmenity", x => new { x.EventId, x.AmenityId });
                    table.ForeignKey(
                        name: "FK_EventAmenity_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventAmenity_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_EventAmenity_AmenityId",
                table: "EventAmenity",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_EventAmenity_EventId",
                table: "EventAmenity",
                column: "EventId");
        }
    }
}
