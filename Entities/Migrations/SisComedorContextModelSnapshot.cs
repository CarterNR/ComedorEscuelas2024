﻿// <auto-generated />
using System;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(SisComedorContext))]
    partial class SisComedorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Entities.Bitacora", b =>
                {
                    b.Property<int>("IdBitacora")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_BITACORA");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBitacora"));

                    b.Property<string>("Accion")
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("ACCION");

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit")
                        .HasColumnName("ESTADO");

                    b.Property<DateTime?>("FechaHora")
                        .HasColumnType("datetime2")
                        .HasColumnName("FECHA_HORA");

                    b.Property<int?>("IdEscuela")
                        .HasColumnType("int")
                        .HasColumnName("ID_ESCUELA");

                    b.Property<int?>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO");

                    b.HasKey("IdBitacora")
                        .HasName("PK__BITACORA__44E70BF34A030070");

                    b.HasIndex("IdEscuela");

                    b.HasIndex("IdUsuario");

                    b.ToTable("BITACORA", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.Escuela", b =>
                {
                    b.Property<int>("IdEscuela")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_ESCUELA");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEscuela"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit")
                        .HasColumnName("ESTADO");

                    b.Property<string>("NombreEscuela")
                        .HasMaxLength(120)
                        .IsUnicode(false)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("NOMBRE_ESCUELA");

                    b.HasKey("IdEscuela")
                        .HasName("PK__ESCUELAS__99BC376F185E0BAA");

                    b.ToTable("ESCUELAS", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.EstadoPedido", b =>
                {
                    b.Property<int>("IdEstadoPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_ESTADO_PEDIDO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEstadoPedido"));

                    b.Property<string>("EstadoPedido1")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("ESTADO_PEDIDO");

                    b.HasKey("IdEstadoPedido")
                        .HasName("PK__ESTADO_P__164DA606AF643A4C");

                    b.ToTable("ESTADO_PEDIDOS", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.Pedido", b =>
                {
                    b.Property<int>("IdPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_PEDIDO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPedido"));

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("CANTIDAD");

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit")
                        .HasColumnName("ESTADO");

                    b.Property<DateTime?>("FechaHoraIngreso")
                        .HasColumnType("datetime2")
                        .HasColumnName("FECHA_HORA_INGRESO");

                    b.Property<int?>("IdEscuela")
                        .HasColumnType("int")
                        .HasColumnName("ID_ESCUELA");

                    b.Property<int?>("IdEstadoPedido")
                        .HasColumnType("int")
                        .HasColumnName("ID_ESTADO_PEDIDO");

                    b.Property<int?>("IdProducto")
                        .HasColumnType("int")
                        .HasColumnName("ID_PRODUCTO");

                    b.Property<int?>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO");

                    b.HasKey("IdPedido")
                        .HasName("PK__PEDIDOS__A05C2F2A5D74C150");

                    b.HasIndex("IdEscuela");

                    b.HasIndex("IdEstadoPedido");

                    b.HasIndex("IdProducto");

                    b.HasIndex("IdUsuario");

                    b.ToTable("PEDIDOS", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.Producto", b =>
                {
                    b.Property<int>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_PRODUCTO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProducto"));

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("CANTIDAD");

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit")
                        .HasColumnName("ESTADO");

                    b.Property<int?>("IdEscuela")
                        .HasColumnType("int")
                        .HasColumnName("ID_ESCUELA");

                    b.Property<int?>("IdProveedor")
                        .HasColumnType("int")
                        .HasColumnName("ID_PROVEEDOR");

                    b.Property<byte[]>("Imagen")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("IMAGEN");

                    b.Property<string>("NombreProducto")
                        .HasMaxLength(120)
                        .IsUnicode(false)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("NOMBRE_PRODUCTO");

                    b.HasKey("IdProducto")
                        .HasName("PK__PRODUCTO__88BD0357B3FB778C");

                    b.HasIndex("IdEscuela");

                    b.HasIndex("IdProveedor");

                    b.ToTable("PRODUCTOS", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.ProductosDium", b =>
                {
                    b.Property<int>("IdProductoDia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_PRODUCTO_DIA");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProductoDia"));

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("CANTIDAD");

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit")
                        .HasColumnName("ESTADO");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime2")
                        .HasColumnName("FECHA");

                    b.Property<int?>("IdEscuela")
                        .HasColumnType("int")
                        .HasColumnName("ID_ESCUELA");

                    b.Property<int?>("IdProducto")
                        .HasColumnType("int")
                        .HasColumnName("ID_PRODUCTO");

                    b.HasKey("IdProductoDia")
                        .HasName("PK__PRODUCTO__4695E6223F79FED1");

                    b.HasIndex("IdEscuela");

                    b.HasIndex("IdProducto");

                    b.ToTable("PRODUCTOS_DIA", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.Proveedore", b =>
                {
                    b.Property<int>("IdProveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_PROVEEDOR");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProveedor"));

                    b.Property<string>("CorreoElectronico")
                        .HasMaxLength(120)
                        .IsUnicode(false)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("CORREO_ELECTRONICO");

                    b.Property<string>("Direccion")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("DIRECCION");

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit")
                        .HasColumnName("ESTADO");

                    b.Property<int?>("IdEscuela")
                        .HasColumnType("int")
                        .HasColumnName("ID_ESCUELA");

                    b.Property<string>("NombreProveedor")
                        .HasMaxLength(120)
                        .IsUnicode(false)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("NOMBRE_PROVEEDOR");

                    b.Property<string>("Telefono")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("TELEFONO");

                    b.HasKey("IdProveedor")
                        .HasName("PK__PROVEEDO__733DB4C42D0EFDB1");

                    b.HasIndex("IdEscuela");

                    b.ToTable("PROVEEDORES", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.Role", b =>
                {
                    b.Property<int>("IdRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_ROL");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRol"));

                    b.Property<string>("NombreRol")
                        .HasMaxLength(120)
                        .IsUnicode(false)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("NOMBRE_ROL");

                    b.HasKey("IdRol")
                        .HasName("PK__ROLES__203B0F6801E05D87");

                    b.ToTable("ROLES", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.TipoCedula", b =>
                {
                    b.Property<int>("IdTipoCedula")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_TIPO_CEDULA");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTipoCedula"));

                    b.Property<string>("TipoCedula1")
                        .HasMaxLength(120)
                        .IsUnicode(false)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("TIPO_CEDULA");

                    b.HasKey("IdTipoCedula")
                        .HasName("PK__TIPO_CED__0CDCFB15550C3FBE");

                    b.ToTable("TIPO_CEDULA", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Cedula")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CEDULA");

                    b.Property<string>("Clave")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("CLAVE");

                    b.Property<string>("CorreoElectronico")
                        .HasMaxLength(120)
                        .IsUnicode(false)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("CORREO_ELECTRONICO");

                    b.Property<string>("Direccion")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("DIRECCION");

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit")
                        .HasColumnName("ESTADO");

                    b.Property<int?>("IdEscuela")
                        .HasColumnType("int")
                        .HasColumnName("ID_ESCUELA");

                    b.Property<int?>("IdRol")
                        .HasColumnType("int")
                        .HasColumnName("ID_ROL");

                    b.Property<int?>("IdTipoCedula")
                        .HasColumnType("int")
                        .HasColumnName("ID_TIPO_CEDULA");

                    b.Property<string>("NombreCompleto")
                        .HasMaxLength(120)
                        .IsUnicode(false)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("NOMBRE_COMPLETO");

                    b.Property<string>("Telefono")
                        .HasMaxLength(120)
                        .IsUnicode(false)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("TELEFONO");

                    b.HasKey("IdUsuario")
                        .HasName("PK__USUARIOS__91136B908550F84D");

                    b.HasIndex("IdEscuela");

                    b.HasIndex("IdRol");

                    b.HasIndex("IdTipoCedula");

                    b.ToTable("USUARIOS", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.Bitacora", b =>
                {
                    b.HasOne("Entities.Entities.Escuela", "IdEscuelaNavigation")
                        .WithMany("Bitacoras")
                        .HasForeignKey("IdEscuela")
                        .HasConstraintName("FK__BITACORA__ID_ESC__4E88ABD4");

                    b.HasOne("Entities.Entities.Usuario", "IdUsuarioNavigation")
                        .WithMany("Bitacoras")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK__BITACORA__ID_USU__4F7CD00D");

                    b.Navigation("IdEscuelaNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Entities.Entities.Pedido", b =>
                {
                    b.HasOne("Entities.Entities.Escuela", "IdEscuelaNavigation")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdEscuela")
                        .HasConstraintName("FK__PEDIDOS__ID_ESCU__534D60F1");

                    b.HasOne("Entities.Entities.EstadoPedido", "IdEstadoPedidoNavigation")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdEstadoPedido")
                        .HasConstraintName("FK__PEDIDOS__ID_ESTA__5441852A");

                    b.HasOne("Entities.Entities.Producto", "IdProductoNavigation")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdProducto")
                        .HasConstraintName("FK__PEDIDOS__ID_PROD__5535A963");

                    b.HasOne("Entities.Entities.Usuario", "IdUsuarioNavigation")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK__PEDIDOS__ID_USUA__52593CB8");

                    b.Navigation("IdEscuelaNavigation");

                    b.Navigation("IdEstadoPedidoNavigation");

                    b.Navigation("IdProductoNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Entities.Entities.Producto", b =>
                {
                    b.HasOne("Entities.Entities.Escuela", "IdEscuelaNavigation")
                        .WithMany("Productos")
                        .HasForeignKey("IdEscuela")
                        .HasConstraintName("FK__PRODUCTOS__ID_ES__4316F928");

                    b.HasOne("Entities.Entities.Proveedore", "IdProveedorNavigation")
                        .WithMany("Productos")
                        .HasForeignKey("IdProveedor")
                        .HasConstraintName("FK__PRODUCTOS__ID_PR__4222D4EF");

                    b.Navigation("IdEscuelaNavigation");

                    b.Navigation("IdProveedorNavigation");
                });

            modelBuilder.Entity("Entities.Entities.ProductosDium", b =>
                {
                    b.HasOne("Entities.Entities.Escuela", "IdEscuelaNavigation")
                        .WithMany("ProductosDia")
                        .HasForeignKey("IdEscuela")
                        .HasConstraintName("FK__PRODUCTOS__ID_ES__45F365D3");

                    b.HasOne("Entities.Entities.Producto", "IdProductoNavigation")
                        .WithMany("ProductosDia")
                        .HasForeignKey("IdProducto")
                        .HasConstraintName("FK__PRODUCTOS__ID_PR__46E78A0C");

                    b.Navigation("IdEscuelaNavigation");

                    b.Navigation("IdProductoNavigation");
                });

            modelBuilder.Entity("Entities.Entities.Proveedore", b =>
                {
                    b.HasOne("Entities.Entities.Escuela", "IdEscuelaNavigation")
                        .WithMany("Proveedores")
                        .HasForeignKey("IdEscuela")
                        .HasConstraintName("FK__PROVEEDOR__ID_ES__3F466844");

                    b.Navigation("IdEscuelaNavigation");
                });

            modelBuilder.Entity("Entities.Entities.Usuario", b =>
                {
                    b.HasOne("Entities.Entities.Escuela", "IdEscuelaNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdEscuela")
                        .HasConstraintName("FK__USUARIOS__ID_ROL__49C3F6B7");

                    b.HasOne("Entities.Entities.Role", "IdRolNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdRol")
                        .HasConstraintName("FK__USUARIOS__ID_ROL__4AB81AF0");

                    b.HasOne("Entities.Entities.TipoCedula", "IdTipoCedulaNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdTipoCedula")
                        .HasConstraintName("FK__USUARIOS__ID_TIP__4BAC3F29");

                    b.Navigation("IdEscuelaNavigation");

                    b.Navigation("IdRolNavigation");

                    b.Navigation("IdTipoCedulaNavigation");
                });

            modelBuilder.Entity("Entities.Entities.Escuela", b =>
                {
                    b.Navigation("Bitacoras");

                    b.Navigation("Pedidos");

                    b.Navigation("Productos");

                    b.Navigation("ProductosDia");

                    b.Navigation("Proveedores");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Entities.Entities.EstadoPedido", b =>
                {
                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("Entities.Entities.Producto", b =>
                {
                    b.Navigation("Pedidos");

                    b.Navigation("ProductosDia");
                });

            modelBuilder.Entity("Entities.Entities.Proveedore", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("Entities.Entities.Role", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Entities.Entities.TipoCedula", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Entities.Entities.Usuario", b =>
                {
                    b.Navigation("Bitacoras");

                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
