using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat_WEB2_backend.Migrations
{
    public partial class ProdavnicaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipKorisnika = table.Column<int>(type: "int", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusVerifikacije = table.Column<int>(type: "int", nullable: false),
                    CenaDostave = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artikli",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    CenaDostave = table.Column<double>(type: "float", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Fotografija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdavacId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikli", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artikli_Korisnici_ProdavacId",
                        column: x => x.ProdavacId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Komentar = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumDostave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StanjePorudzbine = table.Column<int>(type: "int", nullable: false),
                    CenaPorudzbine = table.Column<double>(type: "float", nullable: false),
                    KorisnikId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Porudzbine_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtikliUPorudzbinama",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtikalId = table.Column<long>(type: "bigint", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    PorudzbinaId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtikliUPorudzbinama", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtikliUPorudzbinama_Porudzbine_PorudzbinaId",
                        column: x => x.PorudzbinaId,
                        principalTable: "Porudzbine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artikli_ProdavacId",
                table: "Artikli",
                column: "ProdavacId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtikliUPorudzbinama_PorudzbinaId",
                table: "ArtikliUPorudzbinama",
                column: "PorudzbinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_Email",
                table: "Korisnici",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_KorisnikId",
                table: "Porudzbine",
                column: "KorisnikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artikli");

            migrationBuilder.DropTable(
                name: "ArtikliUPorudzbinama");

            migrationBuilder.DropTable(
                name: "Porudzbine");

            migrationBuilder.DropTable(
                name: "Korisnici");
        }
    }
}
