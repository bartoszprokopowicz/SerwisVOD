using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VOD.Models;

namespace VOD.Data
{
    public class ApplicationDbContext : IdentityDbContext<Uzytkownicy, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aktorzy> Aktorzy { get; set; }
        public virtual DbSet<AktorzyFilmy> AktorzyFilmy { get; set; }
        public virtual DbSet<Daneosobowe> Daneosobowe { get; set; }
        public virtual DbSet<Filmy> Filmy { get; set; }
        public virtual DbSet<Gatunki> Gatunki { get; set; }
        public virtual DbSet<Kraje> Kraje { get; set; }
        public virtual DbSet<KrajeFilmy> KrajeFilmy { get; set; }
        public virtual DbSet<OcenaFilm> OcenaFilm { get; set; }
        public virtual DbSet<Rezyserzy> Rezyserzy { get; set; }
        public virtual DbSet<RezyserzyFilmy> RezyserzyFilmy { get; set; }
        public virtual DbSet<Uzytkownicy> Uzytkownicy { get; set; }
        public virtual DbSet<UzytkownicyFilmy> UzytkownicyFilmy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Uzytkownicy>(entity =>
            {
                entity.ToTable(name: "Uzytkownicy");
                entity.Property(p => p.Id).HasColumnName("UzytkownikID");
                entity.Property(p => p.UserName).HasColumnName("Login");
                entity.Property(p => p.UserName).HasMaxLength(40);
                entity.Property(p => p.NormalizedUserName).HasMaxLength(40);
                entity.Property(p => p.PhoneNumber).HasColumnName("NumerTelefonu");
                entity.Property(p => p.PhoneNumberConfirmed).HasColumnName("PotwierdzenieNumeru");
                entity.Property(p => p.EmailConfirmed).HasColumnName("PotwierdzenieEmail");
                entity.Property(p => p.LockoutEnd).HasColumnName("KoniecBlokady");
                entity.Property(p => p.NormalizedEmail).HasColumnName("ZnormalizowanyEmail");
                entity.Property(p => p.NormalizedUserName).HasColumnName("ZnormalizowanyLogin");
                entity.Property(p => p.Email).HasMaxLength(60);
                entity.Property(p => p.NormalizedEmail).HasMaxLength(60);
            });

            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Role");
                entity.Property(p => p.Id).HasColumnName("RolaID");
                entity.Property(p => p.Name).HasColumnName("Nazwa");
                entity.Property(p => p.NormalizedName).HasColumnName("ZnormalizowanaNazwa");
            });

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("Uzytkownicy_Role");
                entity.HasKey(key => new { key.UserId, key.RoleId });
                entity.Property(p => p.UserId).HasColumnName("UzytkownikID");
                entity.Property(p => p.RoleId).HasColumnName("RolaID");

                //  entity.HasKey(key => new { key.UserId, key.RoleId });
            });

            modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogins");
                //in case you chagned the TKey type
                entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });       
            });


            modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserTokens");
                //in case you chagned the TKey type
                entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });

            });

            modelBuilder.Entity<Aktorzy>(entity =>
            {
                entity.HasKey(e => e.AktorId);

                entity.ToTable("aktorzy", "projektdb2");

                entity.HasIndex(e => e.DaneosoboweId)
                    .HasName("fk_Aktorzy_Daneosobowe_idx");

                entity.Property(e => e.AktorId)
                    .HasColumnName("AktorID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DaneosoboweId)
                    .HasColumnName("DaneosoboweID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Daneosobowe)
                    .WithMany(p => p.Aktorzy)
                    .HasForeignKey(d => d.DaneosoboweId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Aktorzy_Daneosobowe");
            });

            modelBuilder.Entity<AktorzyFilmy>(entity =>
            {
                entity.HasKey(e => new { e.FilmId, e.AktorId });

                entity.ToTable("aktorzy_filmy", "projektdb2");

                entity.HasIndex(e => e.AktorId)
                    .HasName("fk_Aktorzy_Filmy_Aktorzy_idx");

                entity.HasIndex(e => e.FilmId)
                    .HasName("fk_Aktorzy_Filmy_Filmy_idx");

                entity.Property(e => e.FilmId)
                    .HasColumnName("FilmID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AktorId)
                    .HasColumnName("AktorID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Aktor)
                    .WithMany(p => p.AktorzyFilmy)
                    .HasForeignKey(d => d.AktorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Aktorzy_Filmy_Aktorzy");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.AktorzyFilmy)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Aktorzy_Filmy_Filmy");
            });

            modelBuilder.Entity<Daneosobowe>(entity =>
            {
                entity.ToTable("daneosobowe", "projektdb2");

                entity.HasIndex(e => new { e.Nazwisko, e.Imie })
                    .HasName("Nazwisko_imie");

                entity.Property(e => e.DaneosoboweId)
                    .HasColumnName("DaneosoboweID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataUrodzin)
                    .HasColumnName("Data_Urodzin")
                    .HasColumnType("date");

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Filmy>(entity =>
            {
                entity.HasKey(e => e.FilmId);

                entity.ToTable("filmy", "projektdb2");

                entity.HasIndex(e => e.GatunekId)
                    .HasName("FK_Filmy_GatunkiID");

                entity.HasIndex(e => e.PracownikId)
                    .HasName("FK_Filmy_Pracownicy_PracownicyID");

                entity.HasIndex(e => new { e.TytulOrg, e.DataPremiery })
                    .HasName("Tytul_data");

                entity.Property(e => e.FilmId)
                    .HasColumnName("FilmID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cena).HasColumnType("decimal(5,2)");

                entity.Property(e => e.DataPremiery)
                    .HasColumnName("Data_Premiery")
                    .HasColumnType("date");

                entity.Property(e => e.GatunekId)
                    .HasColumnName("GatunekID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Opis)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Plakat)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PracownikId)
                    .HasColumnName("PracownikID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TytulOrg)
                    .IsRequired()
                    .HasColumnName("Tytul_Org")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TytulPol)
                    .HasColumnName("Tytul_Pol")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Gatunek)
                    .WithMany(p => p.Filmy)
                    .HasForeignKey(d => d.GatunekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Filmy_GatunkiID");
            });

            modelBuilder.Entity<Gatunki>(entity =>
            {
                entity.HasKey(e => e.GatunekId);

                entity.ToTable("gatunki", "projektdb2");

                entity.Property(e => e.GatunekId)
                    .HasColumnName("GatunekID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Gatunek)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Kraje>(entity =>
            {
                entity.HasKey(e => e.KrajId);

                entity.ToTable("kraje", "projektdb2");

                entity.HasIndex(e => e.KrajNazwa)
                    .HasName("kk");

                entity.Property(e => e.KrajId)
                    .HasColumnName("KrajID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.KrajKod)
                    .IsRequired()
                    .HasColumnName("Kraj_kod")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.KrajNazwa)
                    .IsRequired()
                    .HasColumnName("Kraj_nazwa")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<KrajeFilmy>(entity =>
            {
                entity.HasKey(e => new { e.FilmId, e.KrajId });

                entity.ToTable("kraje_filmy", "projektdb2");

                entity.HasIndex(e => e.FilmId)
                    .HasName("fk_Kraje_Filmy_Filmy_idx");

                entity.HasIndex(e => e.KrajId)
                    .HasName("fk_Kraje_Filmy_Kraje_idx");

                entity.Property(e => e.FilmId)
                    .HasColumnName("FilmID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.KrajId)
                    .HasColumnName("KrajID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.KrajeFilmy)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Kraje_Filmy_Filmy");

                entity.HasOne(d => d.Kraj)
                    .WithMany(p => p.KrajeFilmy)
                    .HasForeignKey(d => d.KrajId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Kraje_Filmy_Kraje");
            });

            modelBuilder.Entity<OcenaFilm>(entity =>
            {
                entity.HasKey(e => e.FilmId);

                entity.ToTable("ocena_film", "projektdb2");

                entity.Property(e => e.FilmId)
                    .HasColumnName("FilmID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ocena).HasColumnType("decimal(2,1)");

                entity.HasOne(d => d.Film)
                    .WithOne(p => p.OcenaFilm)
                    .HasForeignKey<OcenaFilm>(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ocena_Film_film");
            });

            modelBuilder.Entity<Rezyserzy>(entity =>
            {
                entity.HasKey(e => e.RezyserId);

                entity.ToTable("rezyserzy", "projektdb2");

                entity.HasIndex(e => e.DaneosoboweId)
                    .HasName("fk_Rezyserzy_Daneosobowe_idx");

                entity.Property(e => e.RezyserId)
                    .HasColumnName("RezyserID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DaneosoboweId)
                    .HasColumnName("DaneosoboweID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Daneosobowe)
                    .WithMany(p => p.Rezyserzy)
                    .HasForeignKey(d => d.DaneosoboweId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Rezyserzy_Daneosobowe");
            });

            modelBuilder.Entity<RezyserzyFilmy>(entity =>
            {
                entity.HasKey(e => new { e.FilmId, e.RezyserId });

                entity.ToTable("rezyserzy_filmy", "projektdb2");

                entity.HasIndex(e => e.FilmId)
                    .HasName("fk_Rezyserzy_Filmy_Filmy_idx");

                entity.HasIndex(e => e.RezyserId)
                    .HasName("fk_Rezyserzy_Filmy_Rezyserzy_idx");

                entity.Property(e => e.FilmId)
                    .HasColumnName("FilmID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RezyserId)
                    .HasColumnName("RezyserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.RezyserzyFilmy)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Rezyserzy_Filmy_Filmy");

                entity.HasOne(d => d.Rezyser)
                    .WithMany(p => p.RezyserzyFilmy)
                    .HasForeignKey(d => d.RezyserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Rezyserzy_Filmy_Rezyserzy");
            });

            modelBuilder.Entity<UzytkownicyFilmy>(entity =>
            {
                entity.HasKey(e => new { e.UzytkownikId, e.FilmId });

                entity.ToTable("uzytkownicy_filmy", "projektdb2");

                entity.HasIndex(e => e.FilmId)
                    .HasName("fk_Uzytkownicy_Filmy_Filmy_idx");

                entity.HasIndex(e => e.UzytkownikId)
                    .HasName("fk_Uzytkownicy_Filmy_Uzytkownicy_idx");

                entity.Property(e => e.UzytkownikId)
                    .HasColumnName("UzytkownikID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FilmId)
                    .HasColumnName("FilmID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Ocena).HasColumnType("decimal(2,1)");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.UzytkownicyFilmy)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Uzytkownicy_Filmy_Filmy");

                entity.HasOne(d => d.Uzytkownik)
                    .WithMany(p => p.UzytkownicyFilmy)
                    .HasForeignKey(d => d.UzytkownikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Uzytkownicy_Filmy_Uzytkownicy");
            });
            
        }
    }
}
