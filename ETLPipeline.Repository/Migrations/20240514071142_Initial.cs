using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETLPipeline.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icao24 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Callsign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimePosition = table.Column<int>(type: "int", nullable: true),
                    LastContact = table.Column<int>(type: "int", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    BaroAltitude = table.Column<double>(type: "float", nullable: true),
                    OnGround = table.Column<bool>(type: "bit", nullable: true),
                    Velocity = table.Column<double>(type: "float", nullable: true),
                    TrueTrack = table.Column<double>(type: "float", nullable: true),
                    VerticalRate = table.Column<double>(type: "float", nullable: true),
                    Sensors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeoAltitude = table.Column<double>(type: "float", nullable: true),
                    Squawk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spi = table.Column<bool>(type: "bit", nullable: true),
                    PositionSource = table.Column<int>(type: "int", nullable: true),
                    CollectedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
