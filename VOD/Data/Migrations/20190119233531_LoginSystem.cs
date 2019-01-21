using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VOD.Data.Migrations
{
    public partial class LoginSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "projektdb2");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RolaID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(maxLength: 256, nullable: true),
                    ZnormalizowanaNazwa = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RolaID);
                });

            migrationBuilder.CreateTable(
                name: "daneosobowe",
                schema: "projektdb2",
                columns: table => new
                {
                    DaneosoboweID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Imie = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Nazwisko = table.Column<string>(unicode: false, maxLength: 60, nullable: false),
                    Data_Urodzin = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_daneosobowe", x => x.DaneosoboweID);
                });

            migrationBuilder.CreateTable(
                name: "gatunki",
                schema: "projektdb2",
                columns: table => new
                {
                    GatunekID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Gatunek = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gatunki", x => x.GatunekID);
                });

            migrationBuilder.CreateTable(
                name: "kraje",
                schema: "projektdb2",
                columns: table => new
                {
                    KrajID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Kraj_kod = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
                    Kraj_nazwa = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kraje", x => x.KrajID);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RolaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownicy",
                columns: table => new
                {
                    UzytkownikID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(maxLength: 40, nullable: true),
                    ZnormalizowanyLogin = table.Column<string>(maxLength: 40, nullable: true),
                    Email = table.Column<string>(maxLength: 60, nullable: true),
                    ZnormalizowanyEmail = table.Column<string>(maxLength: 60, nullable: true),
                    PotwierdzenieEmail = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    NumerTelefonu = table.Column<string>(nullable: true),
                    PotwierdzenieNumeru = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    KoniecBlokady = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    DataUtworzenia = table.Column<DateTime>(nullable: false),
                    DaneosoboweId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownicy", x => x.UzytkownikID);
                    table.ForeignKey(
                        name: "FK_Uzytkownicy_daneosobowe_DaneosoboweId",
                        column: x => x.DaneosoboweId,
                        principalSchema: "projektdb2",
                        principalTable: "daneosobowe",
                        principalColumn: "DaneosoboweID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aktorzy",
                schema: "projektdb2",
                columns: table => new
                {
                    AktorID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DaneosoboweID = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aktorzy", x => x.AktorID);
                    table.ForeignKey(
                        name: "fk_Aktorzy_Daneosobowe",
                        column: x => x.DaneosoboweID,
                        principalSchema: "projektdb2",
                        principalTable: "daneosobowe",
                        principalColumn: "DaneosoboweID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rezyserzy",
                schema: "projektdb2",
                columns: table => new
                {
                    RezyserID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DaneosoboweID = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rezyserzy", x => x.RezyserID);
                    table.ForeignKey(
                        name: "fk_Rezyserzy_Daneosobowe",
                        column: x => x.DaneosoboweID,
                        principalSchema: "projektdb2",
                        principalTable: "daneosobowe",
                        principalColumn: "DaneosoboweID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Uzytkownicy_UserId",
                        column: x => x.UserId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "UzytkownikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.ProviderKey, x.LoginProvider });
                    table.UniqueConstraint("AK_UserLogins_LoginProvider_ProviderKey", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Uzytkownicy_UserId",
                        column: x => x.UserId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "UzytkownikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Uzytkownicy_UserId",
                        column: x => x.UserId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "UzytkownikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownicy_Role",
                columns: table => new
                {
                    UzytkownikID = table.Column<int>(nullable: false),
                    RolaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownicy_Role", x => new { x.UzytkownikID, x.RolaID });
                    table.ForeignKey(
                        name: "FK_Uzytkownicy_Role_Role_RolaID",
                        column: x => x.RolaID,
                        principalTable: "Role",
                        principalColumn: "RolaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uzytkownicy_Role_Uzytkownicy_UzytkownikID",
                        column: x => x.UzytkownikID,
                        principalTable: "Uzytkownicy",
                        principalColumn: "UzytkownikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "filmy",
                schema: "projektdb2",
                columns: table => new
                {
                    FilmID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tytul_Org = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Tytul_Pol = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Opis = table.Column<string>(unicode: false, nullable: false),
                    Plakat = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    Cena = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Data_Premiery = table.Column<DateTime>(type: "date", nullable: false),
                    GatunekID = table.Column<int>(type: "int(11)", nullable: false),
                    PracownikID = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filmy", x => x.FilmID);
                    table.ForeignKey(
                        name: "FK_Filmy_GatunkiID",
                        column: x => x.GatunekID,
                        principalSchema: "projektdb2",
                        principalTable: "gatunki",
                        principalColumn: "GatunekID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_filmy_Uzytkownicy_PracownikID",
                        column: x => x.PracownikID,
                        principalTable: "Uzytkownicy",
                        principalColumn: "UzytkownikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aktorzy_filmy",
                schema: "projektdb2",
                columns: table => new
                {
                    FilmID = table.Column<int>(type: "int(11)", nullable: false),
                    AktorID = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aktorzy_filmy", x => new { x.FilmID, x.AktorID });
                    table.ForeignKey(
                        name: "fk_Aktorzy_Filmy_Aktorzy",
                        column: x => x.AktorID,
                        principalSchema: "projektdb2",
                        principalTable: "aktorzy",
                        principalColumn: "AktorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_Aktorzy_Filmy_Filmy",
                        column: x => x.FilmID,
                        principalSchema: "projektdb2",
                        principalTable: "filmy",
                        principalColumn: "FilmID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "kraje_filmy",
                schema: "projektdb2",
                columns: table => new
                {
                    FilmID = table.Column<int>(type: "int(11)", nullable: false),
                    KrajID = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kraje_filmy", x => new { x.FilmID, x.KrajID });
                    table.ForeignKey(
                        name: "fk_Kraje_Filmy_Filmy",
                        column: x => x.FilmID,
                        principalSchema: "projektdb2",
                        principalTable: "filmy",
                        principalColumn: "FilmID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_Kraje_Filmy_Kraje",
                        column: x => x.KrajID,
                        principalSchema: "projektdb2",
                        principalTable: "kraje",
                        principalColumn: "KrajID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ocena_film",
                schema: "projektdb2",
                columns: table => new
                {
                    FilmID = table.Column<int>(type: "int(11)", nullable: false),
                    Ocena = table.Column<decimal>(type: "decimal(2,1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ocena_film", x => x.FilmID);
                    table.ForeignKey(
                        name: "fk_Ocena_Film_film",
                        column: x => x.FilmID,
                        principalSchema: "projektdb2",
                        principalTable: "filmy",
                        principalColumn: "FilmID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rezyserzy_filmy",
                schema: "projektdb2",
                columns: table => new
                {
                    FilmID = table.Column<int>(type: "int(11)", nullable: false),
                    RezyserID = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rezyserzy_filmy", x => new { x.FilmID, x.RezyserID });
                    table.ForeignKey(
                        name: "fk_Rezyserzy_Filmy_Filmy",
                        column: x => x.FilmID,
                        principalSchema: "projektdb2",
                        principalTable: "filmy",
                        principalColumn: "FilmID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_Rezyserzy_Filmy_Rezyserzy",
                        column: x => x.RezyserID,
                        principalSchema: "projektdb2",
                        principalTable: "rezyserzy",
                        principalColumn: "RezyserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "uzytkownicy_filmy",
                schema: "projektdb2",
                columns: table => new
                {
                    UzytkownikID = table.Column<int>(type: "int(11)", nullable: false),
                    FilmID = table.Column<int>(type: "int(11)", nullable: false),
                    Ocena = table.Column<decimal>(type: "decimal(2,1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uzytkownicy_filmy", x => new { x.UzytkownikID, x.FilmID });
                    table.ForeignKey(
                        name: "fk_Uzytkownicy_Filmy_Filmy",
                        column: x => x.FilmID,
                        principalSchema: "projektdb2",
                        principalTable: "filmy",
                        principalColumn: "FilmID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_Uzytkownicy_Filmy_Uzytkownicy",
                        column: x => x.UzytkownikID,
                        principalTable: "Uzytkownicy",
                        principalColumn: "UzytkownikID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "ZnormalizowanaNazwa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownicy_DaneosoboweId",
                table: "Uzytkownicy",
                column: "DaneosoboweId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Uzytkownicy",
                column: "ZnormalizowanyEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Uzytkownicy",
                column: "ZnormalizowanyLogin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownicy_Role_RolaID",
                table: "Uzytkownicy_Role",
                column: "RolaID");

            migrationBuilder.CreateIndex(
                name: "fk_Aktorzy_Daneosobowe_idx",
                schema: "projektdb2",
                table: "aktorzy",
                column: "DaneosoboweID");

            migrationBuilder.CreateIndex(
                name: "fk_Aktorzy_Filmy_Aktorzy_idx",
                schema: "projektdb2",
                table: "aktorzy_filmy",
                column: "AktorID");

            migrationBuilder.CreateIndex(
                name: "fk_Aktorzy_Filmy_Filmy_idx",
                schema: "projektdb2",
                table: "aktorzy_filmy",
                column: "FilmID");

            migrationBuilder.CreateIndex(
                name: "Nazwisko_imie",
                schema: "projektdb2",
                table: "daneosobowe",
                columns: new[] { "Nazwisko", "Imie" });

            migrationBuilder.CreateIndex(
                name: "FK_Filmy_GatunkiID",
                schema: "projektdb2",
                table: "filmy",
                column: "GatunekID");

            migrationBuilder.CreateIndex(
                name: "FK_Filmy_Pracownicy_PracownicyID",
                schema: "projektdb2",
                table: "filmy",
                column: "PracownikID");

            migrationBuilder.CreateIndex(
                name: "Tytul_data",
                schema: "projektdb2",
                table: "filmy",
                columns: new[] { "Tytul_Org", "Data_Premiery" });

            migrationBuilder.CreateIndex(
                name: "kk",
                schema: "projektdb2",
                table: "kraje",
                column: "Kraj_nazwa");

            migrationBuilder.CreateIndex(
                name: "fk_Kraje_Filmy_Filmy_idx",
                schema: "projektdb2",
                table: "kraje_filmy",
                column: "FilmID");

            migrationBuilder.CreateIndex(
                name: "fk_Kraje_Filmy_Kraje_idx",
                schema: "projektdb2",
                table: "kraje_filmy",
                column: "KrajID");

            migrationBuilder.CreateIndex(
                name: "fk_Rezyserzy_Daneosobowe_idx",
                schema: "projektdb2",
                table: "rezyserzy",
                column: "DaneosoboweID");

            migrationBuilder.CreateIndex(
                name: "fk_Rezyserzy_Filmy_Filmy_idx",
                schema: "projektdb2",
                table: "rezyserzy_filmy",
                column: "FilmID");

            migrationBuilder.CreateIndex(
                name: "fk_Rezyserzy_Filmy_Rezyserzy_idx",
                schema: "projektdb2",
                table: "rezyserzy_filmy",
                column: "RezyserID");

            migrationBuilder.CreateIndex(
                name: "fk_Uzytkownicy_Filmy_Filmy_idx",
                schema: "projektdb2",
                table: "uzytkownicy_filmy",
                column: "FilmID");

            migrationBuilder.CreateIndex(
                name: "fk_Uzytkownicy_Filmy_Uzytkownicy_idx",
                schema: "projektdb2",
                table: "uzytkownicy_filmy",
                column: "UzytkownikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Uzytkownicy_Role");

            migrationBuilder.DropTable(
                name: "aktorzy_filmy",
                schema: "projektdb2");

            migrationBuilder.DropTable(
                name: "kraje_filmy",
                schema: "projektdb2");

            migrationBuilder.DropTable(
                name: "ocena_film",
                schema: "projektdb2");

            migrationBuilder.DropTable(
                name: "rezyserzy_filmy",
                schema: "projektdb2");

            migrationBuilder.DropTable(
                name: "uzytkownicy_filmy",
                schema: "projektdb2");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "aktorzy",
                schema: "projektdb2");

            migrationBuilder.DropTable(
                name: "kraje",
                schema: "projektdb2");

            migrationBuilder.DropTable(
                name: "rezyserzy",
                schema: "projektdb2");

            migrationBuilder.DropTable(
                name: "filmy",
                schema: "projektdb2");

            migrationBuilder.DropTable(
                name: "gatunki",
                schema: "projektdb2");

            migrationBuilder.DropTable(
                name: "Uzytkownicy");

            migrationBuilder.DropTable(
                name: "daneosobowe",
                schema: "projektdb2");
        }
    }
}
