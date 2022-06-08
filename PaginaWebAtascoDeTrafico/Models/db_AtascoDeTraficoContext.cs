using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PaginaWebAtascoDeTrafico.Models
{
    public partial class db_AtascoDeTraficoContext : DbContext
    {
        public db_AtascoDeTraficoContext()
        {
        }

        public db_AtascoDeTraficoContext(DbContextOptions<db_AtascoDeTraficoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbBitacora> TbBitacoras { get; set; }
        public virtual DbSet<TbPuntuaje> TbPuntuajes { get; set; }
        public virtual DbSet<TbUsuario> TbUsuarios { get; set; }
        public virtual DbSet<VistaPuntuajeUsuario> VistaPuntuajeUsuarios { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=db_AtascoDeTrafico;User Id=admin;Password=admin;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<TbBitacora>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tb_bitacora");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Ids)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ids");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(10)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<TbPuntuaje>(entity =>
            {
                entity.HasKey(e => e.IdPuntuaje)
                    .HasName("tb_puntuaje_pkey");

                entity.ToTable("tb_puntuaje");

                entity.Property(e => e.IdPuntuaje).HasColumnName("id_puntuaje");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Puntuaje).HasColumnName("puntuaje");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TbPuntuajes)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("fk_puntuajeusuario");
            });

            modelBuilder.Entity<TbUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("tb_usuario_pkey");

                entity.ToTable("tb_usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(254)
                    .HasColumnName("correo");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("usuario");
            });

            modelBuilder.Entity<VistaPuntuajeUsuario>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vista_puntuaje_usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Puntuaje).HasColumnName("puntuaje");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(30)
                    .HasColumnName("usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
