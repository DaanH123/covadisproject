using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace covadis.Api.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Kenteken = table.Column<string>(type: "TEXT", nullable: false),
                    Merk = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartKM = table.Column<int>(type: "INTEGER", nullable: false),
                    EndKM = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "werknemers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Naam = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    TelefoonNr = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_werknemers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Stad = table.Column<string>(type: "TEXT", nullable: false),
                    Postcode = table.Column<string>(type: "TEXT", nullable: false),
                    Straat = table.Column<string>(type: "TEXT", nullable: false),
                    StraatNr = table.Column<string>(type: "TEXT", nullable: false),
                    RegistrationId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adresses_Registration_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Registration",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ritten",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    registratieId = table.Column<Guid>(type: "TEXT", nullable: false),
                    autoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    bestuurderId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ritten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ritten_Autos_autoId",
                        column: x => x.autoId,
                        principalTable: "Autos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ritten_Registration_registratieId",
                        column: x => x.registratieId,
                        principalTable: "Registration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ritten_werknemers_bestuurderId",
                        column: x => x.bestuurderId,
                        principalTable: "werknemers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_RegistrationId",
                table: "Adresses",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Ritten_autoId",
                table: "Ritten",
                column: "autoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ritten_bestuurderId",
                table: "Ritten",
                column: "bestuurderId");

            migrationBuilder.CreateIndex(
                name: "IX_Ritten_registratieId",
                table: "Ritten",
                column: "registratieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "Ritten");

            migrationBuilder.DropTable(
                name: "Autos");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "werknemers");
        }
    }
}
