using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Facturacion.iso810.Models
{
    public partial class facturacioncompletaiso810Context : DbContext
    {
        public facturacioncompletaiso810Context()
        {
        }

        public facturacioncompletaiso810Context(DbContextOptions<facturacioncompletaiso810Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulo> Articulos { get; set; } = null!;
        public virtual DbSet<AsientoContable> AsientoContables { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Cuentum> Cuenta { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<Vendedor> Vendedors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
  //              optionsBuilder.UseSqlServer("server=LAPTOP-MA24ETJT\\ABREUDATA; database=facturacioncompletaiso810; integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.ToTable("Articulo");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<AsientoContable>(entity =>
            {
                entity.HasKey(e => e.IdAsiento)
                    .HasName("PK__AsientoC__F5678721520843E0");

                entity.ToTable("AsientoContable");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TipoMovimiento).HasMaxLength(10);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.AsientoContables)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AsientoCo__Clien__2E1BDC42");

                entity.HasOne(d => d.Cuenta)
                    .WithMany(p => p.AsientoContables)
                    .HasForeignKey(d => d.CuentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AsientoCo__Cuent__2F10007B");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.CedulaRnc)
                    .HasMaxLength(50)
                    .HasColumnName("CedulaRNC");

                entity.Property(e => e.CuentaContable).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Cuentum>(entity =>
            {
                entity.Property(e => e.NoCuenta).HasMaxLength(50);
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Factura");

                entity.Property(e => e.Comentario).HasMaxLength(255);

                entity.Property(e => e.DescripcionArticulo).HasMaxLength(255);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Articulo)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.ArticuloId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__Articul__33D4B598");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__Cliente__31EC6D26");

                entity.HasOne(d => d.Vendedor)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.VendedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__Vendedo__32E0915F");
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.ToTable("Vendedor");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.PorcentajeComision).HasColumnType("decimal(5, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
