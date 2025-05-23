﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities.Entities;

public partial class KyomuContext : DbContext
{
    public KyomuContext()
    {
    }

    public KyomuContext(DbContextOptions<KyomuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Platillo> Platillos { get; set; }

    public virtual DbSet<Reseña> Reseñas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__02AA07851839776D");

            entity.Property(e => e.IdCategoria).HasColumnName("ID_Categoria");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.NombreCategoria).HasMaxLength(50);
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.IdDetallePedido).HasName("PK__Detalle___5ED5F41E7ED25A2C");

            entity.ToTable("Detalle_Pedido");

            entity.Property(e => e.IdDetallePedido).HasColumnName("ID_Detalle_Pedido");
            entity.Property(e => e.IdPedido).HasColumnName("ID_Pedido");
            entity.Property(e => e.IdPlatillo).HasColumnName("ID_Platillo");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Detalle_P__ID_Pe__4BAC3F29");

            entity.HasOne(d => d.IdPlatilloNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPlatillo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Detalle_P__ID_Pl__4CA06362");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodo).HasName("PK__Metodo_P__AE866C416A31BD3B");

            entity.ToTable("Metodo_Pago");

            entity.Property(e => e.IdMetodo).HasColumnName("ID_Metodo");
            entity.Property(e => e.TipoMetodo).HasMaxLength(50);
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pago__AE88B429FECF62AE");

            entity.ToTable("Pago");

            entity.Property(e => e.IdPago).HasColumnName("ID_Pago");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdMetodo).HasColumnName("ID_Metodo");
            entity.Property(e => e.IdPedido).HasColumnName("ID_Pedido");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdMetodoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdMetodo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__ID_Metodo__52593CB8");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__ID_Pedido__5165187F");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedido__FD768070A1268602");

            entity.ToTable("Pedido");

            entity.Property(e => e.IdPedido).HasColumnName("ID_Pedido");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.FechaEntrega).HasColumnType("datetime");
            entity.Property(e => e.FechaPedido)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.TipoEntrega).HasMaxLength(50);
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido__ID_Usuar__47DBAE45");
        });

        modelBuilder.Entity<Platillo>(entity =>
        {
            entity.HasKey(e => e.IdPlatillo).HasName("PK__Platillo__28C8FA3825CFF58B");

            entity.ToTable("Platillo");

            entity.Property(e => e.IdPlatillo).HasColumnName("ID_Platillo");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.IdCategoria).HasColumnName("ID_Categoria");
            entity.Property(e => e.Imagen).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Platillos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Platillo__ID_Cat__3E52440B");
        });

        modelBuilder.Entity<Reseña>(entity =>
        {
            entity.HasKey(e => e.IdReseña).HasName("PK__Reseña__B4FBAB7554B1C1C1");

            entity.ToTable("Reseña");

            entity.Property(e => e.IdReseña).HasColumnName("ID_Reseña");
            entity.Property(e => e.Comentario).HasMaxLength(255);
            entity.Property(e => e.IdPlatillo).HasColumnName("ID_Platillo");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

            entity.HasOne(d => d.IdPlatilloNavigation).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.IdPlatillo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reseña__ID_Plati__571DF1D5");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reseña__ID_Usuar__5629CD9C");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__202AD2200A7898B9");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("ID_Rol");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.NombreRol).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__DE4431C543335568");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Usuario__531402F3BF68C240").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.Contraseña).HasMaxLength(255);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.IdRol).HasColumnName("ID_Rol");
            entity.Property(e => e.Imagen).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(15);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__ID_Rol__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
