using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pagos.Backend.Data;

public partial class DeudaContext : DbContext
{
    public DeudaContext()
    {
    }

    public DeudaContext(DbContextOptions<DeudaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrador> Administradors { get; set; }

    public virtual DbSet<Mensualidad> Mensualidads { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<StatusP> StatusPs { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioServicio> UsuarioServicios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=vicentc0de.com;database=deuda;uid=deuda;pwd=deuda.deuda2525;sslmode=none", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.37-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Administrador>(entity =>
        {
            entity.HasKey(e => e.IdAdministrador).HasName("PRIMARY");

            entity.ToTable("Administrador");

            entity.Property(e => e.IdAdministrador).HasColumnType("int(11)");
            entity.Property(e => e.ContrasenaAdministrador).HasMaxLength(256);
            entity.Property(e => e.NombreAdministrador).HasMaxLength(50);
        });

        modelBuilder.Entity<Mensualidad>(entity =>
        {
            entity.HasKey(e => e.IdMensualidad).HasName("PRIMARY");

            entity.ToTable("Mensualidad");

            entity.HasIndex(e => e.IdServicio, "IdServicio");

            entity.Property(e => e.IdMensualidad).HasColumnType("int(11)");
            entity.Property(e => e.IdServicio).HasColumnType("int(11)");
            entity.Property(e => e.NombreMensualidad).HasMaxLength(50);
            entity.Property(e => e.PrecioMensualidad).HasPrecision(10, 2);

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Mensualidads)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("Mensualidad_ibfk_1");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PRIMARY");

            entity.HasIndex(e => e.IdMensualidad, "IdMensualidad");

            entity.HasIndex(e => e.IdStatus, "IdStatus");

            entity.HasIndex(e => e.IdUsuario, "IdUsuario");

            entity.Property(e => e.IdPago)
                .HasMaxLength(40)
                .IsFixedLength();
            entity.Property(e => e.IdMensualidad).HasColumnType("int(11)");
            entity.Property(e => e.IdStatus).HasColumnType("int(11)");
            entity.Property(e => e.IdUsuario).HasColumnType("int(11)");
            entity.Property(e => e.PrecioPago).HasPrecision(10, 2);

            entity.HasOne(d => d.IdMensualidadNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdMensualidad)
                .HasConstraintName("Pagos_ibfk_2");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("Pagos_ibfk_1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("Pagos_ibfk_3");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PRIMARY");

            entity.ToTable("Servicio");

            entity.Property(e => e.IdServicio).HasColumnType("int(11)");
            entity.Property(e => e.NombreServicio).HasMaxLength(50);
            entity.Property(e => e.PrecioServicio).HasPrecision(10, 2);
        });

        modelBuilder.Entity<StatusP>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PRIMARY");

            entity.ToTable("StatusP");

            entity.Property(e => e.IdStatus).HasColumnType("int(11)");
            entity.Property(e => e.NombreStatus).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnType("int(11)");
            entity.Property(e => e.ApellidoUsuario).HasMaxLength(50);
            entity.Property(e => e.NombresUsuario).HasMaxLength(100);
            entity.Property(e => e.TelefonoUsuario).HasMaxLength(256);
        });

        modelBuilder.Entity<UsuarioServicio>(entity =>
        {
            entity.HasKey(e => e.IdUs).HasName("PRIMARY");

            entity.ToTable("UsuarioServicio");

            entity.HasIndex(e => e.IdServicio, "IdServicio");

            entity.HasIndex(e => e.IdUsuario, "IdUsuario");

            entity.Property(e => e.IdUs)
                .HasColumnType("int(11)")
                .HasColumnName("IdUS");
            entity.Property(e => e.IdServicio).HasColumnType("int(11)");
            entity.Property(e => e.IdUsuario).HasColumnType("int(11)");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.UsuarioServicios)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("UsuarioServicio_ibfk_2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioServicios)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("UsuarioServicio_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
