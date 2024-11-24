using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "departman",
                columns: table => new
                {
                    departman_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ad = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    aciklama = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.departman_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "pozisyon",
                columns: table => new
                {
                    pozisyon_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ad = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    aciklama = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    departman_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.pozisyon_id);
                    table.ForeignKey(
                        name: "FK_pozisyon_departman_departman_id",
                        column: x => x.departman_id,
                        principalTable: "departman",
                        principalColumn: "departman_id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "personel",
                columns: table => new
                {
                    personel_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tckn = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ad = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    soyad = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dogum_tarihi = table.Column<DateOnly>(type: "date", nullable: true),
                    cinsiyet = table.Column<string>(type: "enum('E','K')", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    departman_id = table.Column<int>(type: "int(11)", nullable: false),
                    pozisyon_id = table.Column<int>(type: "int(11)", nullable: false),
                    Telefon = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eposta = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    iban = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ise_giris_tarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    sabit_maas = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.personel_id);
                    table.ForeignKey(
                        name: "FK_personel_departman_departman_id",
                        column: x => x.departman_id,
                        principalTable: "departman",
                        principalColumn: "departman_id");
                    table.ForeignKey(
                        name: "FK_personel_pozisyon_pozisyon_id",
                        column: x => x.pozisyon_id,
                        principalTable: "pozisyon",
                        principalColumn: "pozisyon_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "izin",
                columns: table => new
                {
                    izin_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    personel_id = table.Column<int>(type: "int(11)", nullable: false),
                    izin_turu = table.Column<string>(type: "enum('Yıllık','Hastalık','Diğer')", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    baslangic_tarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    bitis_tarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    aciklama = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.izin_id);
                    table.ForeignKey(
                        name: "FK_izin_personel_personel_id",
                        column: x => x.personel_id,
                        principalTable: "personel",
                        principalColumn: "personel_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "maas_bordro",
                columns: table => new
                {
                    bordro_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    personel_id = table.Column<int>(type: "int(11)", nullable: false),
                    donem = table.Column<string>(type: "varchar(7)", maxLength: 7, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    brut_maas = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    net_maas = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ek_odemeler = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    kesintiler = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    aciklama = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.bordro_id);
                    table.ForeignKey(
                        name: "FK_maas_bordro_personel_personel_id",
                        column: x => x.personel_id,
                        principalTable: "personel",
                        principalColumn: "personel_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "rapor",
                columns: table => new
                {
                    rapor_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    personel_id = table.Column<int>(type: "int(11)", nullable: false),
                    rapor_turu = table.Column<string>(type: "enum('Devamsızlık','Performans','Diğer')", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rapor_tarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    aciklama = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.rapor_id);
                    table.ForeignKey(
                        name: "FK_rapor_personel_personel_id",
                        column: x => x.personel_id,
                        principalTable: "personel",
                        principalColumn: "personel_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "FK_izin_personel_personel_id",
                table: "izin",
                column: "personel_id");

            migrationBuilder.CreateIndex(
                name: "FK_maas_bordro_personel_personel_id",
                table: "maas_bordro",
                column: "personel_id");

            migrationBuilder.CreateIndex(
                name: "FK_personel_departman_departman_id",
                table: "personel",
                column: "departman_id");

            migrationBuilder.CreateIndex(
                name: "FK_personel_pozisyon_pozisyon_id",
                table: "personel",
                column: "pozisyon_id");

            migrationBuilder.CreateIndex(
                name: "FK_pozisyon_departman_departman_id",
                table: "pozisyon",
                column: "departman_id");

            migrationBuilder.CreateIndex(
                name: "FK_rapor_personel_personel_id",
                table: "rapor",
                column: "personel_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "izin");

            migrationBuilder.DropTable(
                name: "maas_bordro");

            migrationBuilder.DropTable(
                name: "rapor");

            migrationBuilder.DropTable(
                name: "personel");

            migrationBuilder.DropTable(
                name: "pozisyon");

            migrationBuilder.DropTable(
                name: "departman");
        }
    }
}
