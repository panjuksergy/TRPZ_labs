using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitoringService.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemaProds_Movies_MovieId",
                table: "CinemaProds");

            migrationBuilder.DropIndex(
                name: "IX_CinemaProds_MovieId",
                table: "CinemaProds");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "CinemaProds");

            migrationBuilder.AddColumn<Guid>(
                name: "CinemaProdId",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CinemaProdId",
                table: "Movies",
                column: "CinemaProdId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_CinemaProds_CinemaProdId",
                table: "Movies",
                column: "CinemaProdId",
                principalTable: "CinemaProds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_CinemaProds_CinemaProdId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CinemaProdId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CinemaProdId",
                table: "Movies");

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                table: "CinemaProds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CinemaProds_MovieId",
                table: "CinemaProds",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaProds_Movies_MovieId",
                table: "CinemaProds",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
