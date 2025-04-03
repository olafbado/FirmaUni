﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Firma.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aktualnosc",
                columns: table => new
                {
                    IdAktualnosci = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LinkTytul = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Tytul = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Tresc = table.Column<string>(type: "text", nullable: false),
                    Pozycja = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aktualnosc", x => x.IdAktualnosci);
                });

            migrationBuilder.CreateTable(
                name: "Rodzaj",
                columns: table => new
                {
                    IdRodzaju = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nazwa = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Opis = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rodzaj", x => x.IdRodzaju);
                });

            migrationBuilder.CreateTable(
                name: "Strona",
                columns: table => new
                {
                    IdStrony = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LinkTytul = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Tytul = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Tresc = table.Column<string>(type: "text", nullable: false),
                    Pozycja = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strona", x => x.IdStrony);
                });

            migrationBuilder.CreateTable(
                name: "Towar",
                columns: table => new
                {
                    IdTowaru = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Kod = table.Column<string>(type: "text", nullable: false),
                    Nazwa = table.Column<string>(type: "text", nullable: false),
                    Cena = table.Column<decimal>(type: "money", nullable: false),
                    FotoUrl = table.Column<string>(type: "text", nullable: false),
                    Opis = table.Column<string>(type: "text", nullable: false),
                    IdRodzaju = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towar", x => x.IdTowaru);
                    table.ForeignKey(
                        name: "FK_Towar_Rodzaj_IdRodzaju",
                        column: x => x.IdRodzaju,
                        principalTable: "Rodzaj",
                        principalColumn: "IdRodzaju",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Towar_IdRodzaju",
                table: "Towar",
                column: "IdRodzaju");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aktualnosc");

            migrationBuilder.DropTable(
                name: "Strona");

            migrationBuilder.DropTable(
                name: "Towar");

            migrationBuilder.DropTable(
                name: "Rodzaj");
        }
    }
}
