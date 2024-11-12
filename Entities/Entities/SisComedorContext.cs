using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities.Entities;

public partial class SisComedorContext : DbContext
{
    public SisComedorContext()
    {
    }

    public SisComedorContext(DbContextOptions<SisComedorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bitacora> Bitacoras { get; set; }

    public virtual DbSet<Escuela> Escuelas { get; set; }

    public virtual DbSet<EstadoPedido> EstadoPedidos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductosDium> ProductosDia { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TipoCedula> TipoCedulas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-BKO8NNV;Database=SisComedor;Integrated Security=True;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bitacora>(entity =>
        {
            entity.HasKey(e => e.IdBitacora).HasName("PK__BITACORA__44E70BF34A030070");

            entity.ToTable("BITACORA");

            entity.Property(e => e.IdBitacora).HasColumnName("ID_BITACORA");
            entity.Property(e => e.Accion)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("ACCION");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.FechaHora).HasColumnName("FECHA_HORA");
            entity.Property(e => e.IdEscuela).HasColumnName("ID_ESCUELA");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

            entity.HasOne(d => d.IdEscuelaNavigation).WithMany(p => p.Bitacoras)
                .HasForeignKey(d => d.IdEscuela)
                .HasConstraintName("FK__BITACORA__ID_ESC__4E88ABD4");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Bitacoras)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__BITACORA__ID_USU__4F7CD00D");
        });

        modelBuilder.Entity<Escuela>(entity =>
        {
            entity.HasKey(e => e.IdEscuela).HasName("PK__ESCUELAS__99BC376F185E0BAA");

            entity.ToTable("ESCUELAS");

            entity.Property(e => e.IdEscuela).HasColumnName("ID_ESCUELA");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.NombreEscuela)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_ESCUELA");
        });

        modelBuilder.Entity<EstadoPedido>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPedido).HasName("PK__ESTADO_P__164DA606AF643A4C");

            entity.ToTable("ESTADO_PEDIDOS");

            entity.Property(e => e.IdEstadoPedido).HasColumnName("ID_ESTADO_PEDIDO");
            entity.Property(e => e.EstadoPedido1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ESTADO_PEDIDO");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__PEDIDOS__A05C2F2A5D74C150");

            entity.ToTable("PEDIDOS");

            entity.Property(e => e.IdPedido).HasColumnName("ID_PEDIDO");
            entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.FechaHoraIngreso).HasColumnName("FECHA_HORA_INGRESO");
            entity.Property(e => e.IdEscuela).HasColumnName("ID_ESCUELA");
            entity.Property(e => e.IdEstadoPedido).HasColumnName("ID_ESTADO_PEDIDO");
            entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

            entity.HasOne(d => d.IdEscuelaNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdEscuela)
                .HasConstraintName("FK__PEDIDOS__ID_ESCU__534D60F1");

            entity.HasOne(d => d.IdEstadoPedidoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdEstadoPedido)
                .HasConstraintName("FK__PEDIDOS__ID_ESTA__5441852A");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__PEDIDOS__ID_PROD__5535A963");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__PEDIDOS__ID_USUA__52593CB8");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__PRODUCTO__88BD0357B3FB778C");

            entity.ToTable("PRODUCTOS");

            entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");
            entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.IdEscuela).HasColumnName("ID_ESCUELA");
            entity.Property(e => e.IdProveedor).HasColumnName("ID_PROVEEDOR");
            entity.Property(e => e.Imagen).HasColumnName("IMAGEN");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_PRODUCTO");

            entity.HasOne(d => d.IdEscuelaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdEscuela)
                .HasConstraintName("FK__PRODUCTOS__ID_ES__4316F928");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__PRODUCTOS__ID_PR__4222D4EF");
        });

        modelBuilder.Entity<ProductosDium>(entity =>
        {
            entity.HasKey(e => e.IdProductoDia).HasName("PK__PRODUCTO__4695E6223F79FED1");

            entity.ToTable("PRODUCTOS_DIA");

            entity.Property(e => e.IdProductoDia).HasColumnName("ID_PRODUCTO_DIA");
            entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.Fecha).HasColumnName("FECHA");
            entity.Property(e => e.IdEscuela).HasColumnName("ID_ESCUELA");
            entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");

            entity.HasOne(d => d.IdEscuelaNavigation).WithMany(p => p.ProductosDia)
                .HasForeignKey(d => d.IdEscuela)
                .HasConstraintName("FK__PRODUCTOS__ID_ES__45F365D3");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductosDia)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__PRODUCTOS__ID_PR__46E78A0C");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__PROVEEDO__733DB4C42D0EFDB1");

            entity.ToTable("PROVEEDORES");

            entity.Property(e => e.IdProveedor).HasColumnName("ID_PROVEEDOR");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("CORREO_ELECTRONICO");
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DIRECCION");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.IdEscuela).HasColumnName("ID_ESCUELA");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_PROVEEDOR");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");

            entity.HasOne(d => d.IdEscuelaNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.IdEscuela)
                .HasConstraintName("FK__PROVEEDOR__ID_ES__3F466844");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__ROLES__203B0F6801E05D87");

            entity.ToTable("ROLES");

            entity.Property(e => e.IdRol).HasColumnName("ID_ROL");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_ROL");
        });

        modelBuilder.Entity<TipoCedula>(entity =>
        {
            entity.HasKey(e => e.IdTipoCedula).HasName("PK__TIPO_CED__0CDCFB15550C3FBE");

            entity.ToTable("TIPO_CEDULA");

            entity.Property(e => e.IdTipoCedula).HasColumnName("ID_TIPO_CEDULA");
            entity.Property(e => e.TipoCedula1)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("TIPO_CEDULA");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIOS__91136B908550F84D");

            entity.ToTable("USUARIOS");

            entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
            entity.Property(e => e.Cedula)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CEDULA");
            entity.Property(e => e.Clave)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("CLAVE");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("CORREO_ELECTRONICO");
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DIRECCION");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.IdEscuela).HasColumnName("ID_ESCUELA");
            entity.Property(e => e.IdRol).HasColumnName("ID_ROL");
            entity.Property(e => e.IdTipoCedula).HasColumnName("ID_TIPO_CEDULA");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_COMPLETO");
            entity.Property(e => e.Telefono)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");

            entity.HasOne(d => d.IdEscuelaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEscuela)
                .HasConstraintName("FK__USUARIOS__ID_ROL__49C3F6B7");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__USUARIOS__ID_ROL__4AB81AF0");

            entity.HasOne(d => d.IdTipoCedulaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTipoCedula)
                .HasConstraintName("FK__USUARIOS__ID_TIP__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
