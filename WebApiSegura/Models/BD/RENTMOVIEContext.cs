using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiSegura.Models.BD
{
    public partial class RENTMOVIEContext : DbContext
    {
        public RENTMOVIEContext()
        {
        }

        public RENTMOVIEContext(DbContextOptions<RENTMOVIEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Pelicula> Pelicula { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\MSSQL;Database=RENTMOVIE;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Idcategoria)
                    .HasName("PK__CATEGORI__ADC0E7198D352DA6");

                entity.ToTable("CATEGORIA");

                entity.Property(e => e.Idcategoria).HasColumnName("IDCATEGORIA");

                entity.Property(e => e.Estado)
                    .HasColumnName("ESTADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.HasKey(e => e.Idpelicula)
                    .HasName("PK__PELICULA__0468E22B08A32C9C");

                entity.ToTable("PELICULA");

                entity.Property(e => e.Idpelicula).HasColumnName("IDPELICULA");

                entity.Property(e => e.Director)
                    .HasColumnName("DIRECTOR")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasColumnName("ESTADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaEstreno)
                    .HasColumnName("FECHA_ESTRENO")
                    .HasColumnType("date");

                entity.Property(e => e.Idcategoria).HasColumnName("IDCATEGORIA");

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnName("RATING");

                entity.HasOne(d => d.IdcategoriaNavigation)
                    .WithMany(p => p.Pelicula)
                    .HasForeignKey(d => d.Idcategoria)
                    .HasConstraintName("FK__PELICULA__IDCATE__15502E78");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.Idreserva)
                    .HasName("PK__RESERVA__98D53B44BACB44A2");

                entity.ToTable("RESERVA");

                entity.Property(e => e.Idreserva).HasColumnName("IDRESERVA");

                entity.Property(e => e.Estado)
                    .HasColumnName("ESTADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaRegis)
                    .HasColumnName("FECHA_REGIS")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Idpelicula).HasColumnName("IDPELICULA");

                entity.Property(e => e.Idusuario).HasColumnName("IDUSUARIO");

                entity.HasOne(d => d.IdpeliculaNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.Idpelicula)
                    .HasConstraintName("FK__RESERVA__IDPELIC__1920BF5C");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.Idusuario)
                    .HasConstraintName("FK__RESERVA__IDUSUAR__1A14E395");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("PK__USUARIO__98242AA9EBF9D37A");

                entity.ToTable("USUARIO");

                entity.Property(e => e.Idusuario).HasColumnName("IDUSUARIO");

                entity.Property(e => e.Apellido)
                    .HasColumnName("APELLIDO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasColumnName("ESTADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rol)
                    .HasColumnName("ROL")
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
