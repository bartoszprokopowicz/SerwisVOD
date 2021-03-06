﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VOD.Data;

namespace VOD.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190119233531_LoginSystem")]
    partial class LoginSystem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("ProviderKey");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("ProviderKey", "LoginProvider");

                    b.HasAlternateKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("UzytkownikID");

                    b.Property<int>("RoleId")
                        .HasColumnName("RolaID");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("Uzytkownicy_Role");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("VOD.Models.Aktorzy", b =>
                {
                    b.Property<int>("AktorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AktorID")
                        .HasColumnType("int(11)");

                    b.Property<int>("DaneosoboweId")
                        .HasColumnName("DaneosoboweID")
                        .HasColumnType("int(11)");

                    b.HasKey("AktorId");

                    b.HasIndex("DaneosoboweId")
                        .HasName("fk_Aktorzy_Daneosobowe_idx");

                    b.ToTable("aktorzy","projektdb2");
                });

            modelBuilder.Entity("VOD.Models.AktorzyFilmy", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnName("FilmID")
                        .HasColumnType("int(11)");

                    b.Property<int>("AktorId")
                        .HasColumnName("AktorID")
                        .HasColumnType("int(11)");

                    b.HasKey("FilmId", "AktorId");

                    b.HasIndex("AktorId")
                        .HasName("fk_Aktorzy_Filmy_Aktorzy_idx");

                    b.HasIndex("FilmId")
                        .HasName("fk_Aktorzy_Filmy_Filmy_idx");

                    b.ToTable("aktorzy_filmy","projektdb2");
                });

            modelBuilder.Entity("VOD.Models.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RolaID");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasColumnName("Nazwa")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnName("ZnormalizowanaNazwa")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("VOD.Models.Daneosobowe", b =>
                {
                    b.Property<int>("DaneosoboweId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DaneosoboweID")
                        .HasColumnType("int(11)");

                    b.Property<DateTime>("DataUrodzin")
                        .HasColumnName("Data_Urodzin")
                        .HasColumnType("date");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false);

                    b.HasKey("DaneosoboweId");

                    b.HasIndex("Nazwisko", "Imie")
                        .HasName("Nazwisko_imie");

                    b.ToTable("daneosobowe","projektdb2");
                });

            modelBuilder.Entity("VOD.Models.Filmy", b =>
                {
                    b.Property<int>("FilmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("FilmID")
                        .HasColumnType("int(11)");

                    b.Property<decimal>("Cena")
                        .HasColumnType("decimal(5,2)");

                    b.Property<DateTime>("DataPremiery")
                        .HasColumnName("Data_Premiery")
                        .HasColumnType("date");

                    b.Property<int>("GatunekId")
                        .HasColumnName("GatunekID")
                        .HasColumnType("int(11)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .IsUnicode(false);

                    b.Property<string>("Plakat")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<int>("PracownikId")
                        .HasColumnName("PracownikID")
                        .HasColumnType("int(11)");

                    b.Property<string>("TytulOrg")
                        .IsRequired()
                        .HasColumnName("Tytul_Org")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("TytulPol")
                        .HasColumnName("Tytul_Pol")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("FilmId");

                    b.HasIndex("GatunekId")
                        .HasName("FK_Filmy_GatunkiID");

                    b.HasIndex("PracownikId")
                        .HasName("FK_Filmy_Pracownicy_PracownicyID");

                    b.HasIndex("TytulOrg", "DataPremiery")
                        .HasName("Tytul_data");

                    b.ToTable("filmy","projektdb2");
                });

            modelBuilder.Entity("VOD.Models.Gatunki", b =>
                {
                    b.Property<int>("GatunekId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("GatunekID")
                        .HasColumnType("int(11)");

                    b.Property<string>("Gatunek")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("GatunekId");

                    b.ToTable("gatunki","projektdb2");
                });

            modelBuilder.Entity("VOD.Models.Kraje", b =>
                {
                    b.Property<int>("KrajId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("KrajID")
                        .HasColumnType("int(11)");

                    b.Property<string>("KrajKod")
                        .IsRequired()
                        .HasColumnName("Kraj_kod")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("KrajNazwa")
                        .IsRequired()
                        .HasColumnName("Kraj_nazwa")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("KrajId");

                    b.HasIndex("KrajNazwa")
                        .HasName("kk");

                    b.ToTable("kraje","projektdb2");
                });

            modelBuilder.Entity("VOD.Models.KrajeFilmy", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnName("FilmID")
                        .HasColumnType("int(11)");

                    b.Property<int>("KrajId")
                        .HasColumnName("KrajID")
                        .HasColumnType("int(11)");

                    b.HasKey("FilmId", "KrajId");

                    b.HasIndex("FilmId")
                        .HasName("fk_Kraje_Filmy_Filmy_idx");

                    b.HasIndex("KrajId")
                        .HasName("fk_Kraje_Filmy_Kraje_idx");

                    b.ToTable("kraje_filmy","projektdb2");
                });

            modelBuilder.Entity("VOD.Models.OcenaFilm", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnName("FilmID")
                        .HasColumnType("int(11)");

                    b.Property<decimal?>("Ocena")
                        .HasColumnType("decimal(2,1)");

                    b.HasKey("FilmId");

                    b.ToTable("ocena_film","projektdb2");
                });

            modelBuilder.Entity("VOD.Models.Rezyserzy", b =>
                {
                    b.Property<int>("RezyserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RezyserID")
                        .HasColumnType("int(11)");

                    b.Property<int>("DaneosoboweId")
                        .HasColumnName("DaneosoboweID")
                        .HasColumnType("int(11)");

                    b.HasKey("RezyserId");

                    b.HasIndex("DaneosoboweId")
                        .HasName("fk_Rezyserzy_Daneosobowe_idx");

                    b.ToTable("rezyserzy","projektdb2");
                });

            modelBuilder.Entity("VOD.Models.RezyserzyFilmy", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnName("FilmID")
                        .HasColumnType("int(11)");

                    b.Property<int>("RezyserId")
                        .HasColumnName("RezyserID")
                        .HasColumnType("int(11)");

                    b.HasKey("FilmId", "RezyserId");

                    b.HasIndex("FilmId")
                        .HasName("fk_Rezyserzy_Filmy_Filmy_idx");

                    b.HasIndex("RezyserId")
                        .HasName("fk_Rezyserzy_Filmy_Rezyserzy_idx");

                    b.ToTable("rezyserzy_filmy","projektdb2");
                });

            modelBuilder.Entity("VOD.Models.Uzytkownicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UzytkownikID");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int>("DaneosoboweId");

                    b.Property<DateTime>("DataUtworzenia");

                    b.Property<string>("Email")
                        .HasMaxLength(60);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnName("PotwierdzenieEmail");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnName("KoniecBlokady");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnName("ZnormalizowanyEmail")
                        .HasMaxLength(60);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnName("ZnormalizowanyLogin")
                        .HasMaxLength(40);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("NumerTelefonu");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnName("PotwierdzenieNumeru");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasColumnName("Login")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.HasIndex("DaneosoboweId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("Uzytkownicy");
                });

            modelBuilder.Entity("VOD.Models.UzytkownicyFilmy", b =>
                {
                    b.Property<int>("UzytkownikId")
                        .HasColumnName("UzytkownikID")
                        .HasColumnType("int(11)");

                    b.Property<int>("FilmId")
                        .HasColumnName("FilmID")
                        .HasColumnType("int(11)");

                    b.Property<decimal?>("Ocena")
                        .HasColumnType("decimal(2,1)");

                    b.HasKey("UzytkownikId", "FilmId");

                    b.HasIndex("FilmId")
                        .HasName("fk_Uzytkownicy_Filmy_Filmy_idx");

                    b.HasIndex("UzytkownikId")
                        .HasName("fk_Uzytkownicy_Filmy_Uzytkownicy_idx");

                    b.ToTable("uzytkownicy_filmy","projektdb2");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("VOD.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("VOD.Models.Uzytkownicy")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("VOD.Models.Uzytkownicy")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("VOD.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VOD.Models.Uzytkownicy")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("VOD.Models.Uzytkownicy")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VOD.Models.Aktorzy", b =>
                {
                    b.HasOne("VOD.Models.Daneosobowe", "Daneosobowe")
                        .WithMany("Aktorzy")
                        .HasForeignKey("DaneosoboweId")
                        .HasConstraintName("fk_Aktorzy_Daneosobowe");
                });

            modelBuilder.Entity("VOD.Models.AktorzyFilmy", b =>
                {
                    b.HasOne("VOD.Models.Aktorzy", "Aktor")
                        .WithMany("AktorzyFilmy")
                        .HasForeignKey("AktorId")
                        .HasConstraintName("fk_Aktorzy_Filmy_Aktorzy");

                    b.HasOne("VOD.Models.Filmy", "Film")
                        .WithMany("AktorzyFilmy")
                        .HasForeignKey("FilmId")
                        .HasConstraintName("fk_Aktorzy_Filmy_Filmy");
                });

            modelBuilder.Entity("VOD.Models.Filmy", b =>
                {
                    b.HasOne("VOD.Models.Gatunki", "Gatunek")
                        .WithMany("Filmy")
                        .HasForeignKey("GatunekId")
                        .HasConstraintName("FK_Filmy_GatunkiID");

                    b.HasOne("VOD.Models.Uzytkownicy", "Pracownik")
                        .WithMany()
                        .HasForeignKey("PracownikId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VOD.Models.KrajeFilmy", b =>
                {
                    b.HasOne("VOD.Models.Filmy", "Film")
                        .WithMany("KrajeFilmy")
                        .HasForeignKey("FilmId")
                        .HasConstraintName("fk_Kraje_Filmy_Filmy");

                    b.HasOne("VOD.Models.Kraje", "Kraj")
                        .WithMany("KrajeFilmy")
                        .HasForeignKey("KrajId")
                        .HasConstraintName("fk_Kraje_Filmy_Kraje");
                });

            modelBuilder.Entity("VOD.Models.OcenaFilm", b =>
                {
                    b.HasOne("VOD.Models.Filmy", "Film")
                        .WithOne("OcenaFilm")
                        .HasForeignKey("VOD.Models.OcenaFilm", "FilmId")
                        .HasConstraintName("fk_Ocena_Film_film");
                });

            modelBuilder.Entity("VOD.Models.Rezyserzy", b =>
                {
                    b.HasOne("VOD.Models.Daneosobowe", "Daneosobowe")
                        .WithMany("Rezyserzy")
                        .HasForeignKey("DaneosoboweId")
                        .HasConstraintName("fk_Rezyserzy_Daneosobowe");
                });

            modelBuilder.Entity("VOD.Models.RezyserzyFilmy", b =>
                {
                    b.HasOne("VOD.Models.Filmy", "Film")
                        .WithMany("RezyserzyFilmy")
                        .HasForeignKey("FilmId")
                        .HasConstraintName("fk_Rezyserzy_Filmy_Filmy");

                    b.HasOne("VOD.Models.Rezyserzy", "Rezyser")
                        .WithMany("RezyserzyFilmy")
                        .HasForeignKey("RezyserId")
                        .HasConstraintName("fk_Rezyserzy_Filmy_Rezyserzy");
                });

            modelBuilder.Entity("VOD.Models.Uzytkownicy", b =>
                {
                    b.HasOne("VOD.Models.Daneosobowe", "Daneosobowe")
                        .WithMany("Uzytkownicy")
                        .HasForeignKey("DaneosoboweId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VOD.Models.UzytkownicyFilmy", b =>
                {
                    b.HasOne("VOD.Models.Filmy", "Film")
                        .WithMany("UzytkownicyFilmy")
                        .HasForeignKey("FilmId")
                        .HasConstraintName("fk_Uzytkownicy_Filmy_Filmy");

                    b.HasOne("VOD.Models.Uzytkownicy", "Uzytkownik")
                        .WithMany("UzytkownicyFilmy")
                        .HasForeignKey("UzytkownikId")
                        .HasConstraintName("fk_Uzytkownicy_Filmy_Uzytkownicy");
                });
#pragma warning restore 612, 618
        }
    }
}
