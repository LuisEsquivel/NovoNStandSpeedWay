using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace Api.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activo> Activos { get; set; }
        public virtual DbSet<CentroCosto> CentroCostos { get; set; }
        public virtual DbSet<EpcProductosRel> EpcProductosRels { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<FormaAdquisicion> FormaAdquisicions { get; set; }
        public virtual DbSet<Lectore> Lectores { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Ubicacione> Ubicaciones { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Activo>(entity =>
            {
                entity.HasKey(e => e.ActivoIdInt);

                entity.Property(e => e.ActivoIdInt).HasColumnName("activoID_int");

                entity.Property(e => e.Adicional10Var)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("adicional10_var");

                entity.Property(e => e.Adicional11Dec)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("adicional11_dec");

                entity.Property(e => e.Adicional12Dec)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("adicional12_dec");

                entity.Property(e => e.Adicional13Dec)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("adicional13_dec");

                entity.Property(e => e.Adicional14Dec)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("adicional14_dec");

                entity.Property(e => e.Adicional15Date)
                    .HasColumnType("datetime")
                    .HasColumnName("adicional15_date");

                entity.Property(e => e.Adicional16Date)
                    .HasColumnType("datetime")
                    .HasColumnName("adicional16_date");

                entity.Property(e => e.Adicional2Var)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("adicional2_var");

                entity.Property(e => e.Adicional3Var)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("adicional3_var");

                entity.Property(e => e.Adicional4Var)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("adicional4_var");

                entity.Property(e => e.Adicional5Var)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("adicional5_var");

                entity.Property(e => e.Adicional6Var)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("adicional6_var");

                entity.Property(e => e.Adicional7Var)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("adicional7_var");

                entity.Property(e => e.Adicional8Var)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("adicional8_var");

                entity.Property(e => e.Adicional9Var)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("adicional9_var");

                entity.Property(e => e.AdicionalVar)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("adicional_var");

                entity.Property(e => e.BarcodeVar)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("barcode_var");

                entity.Property(e => e.CentroCostosIdVar)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("centroCostosID_var");

                entity.Property(e => e.CostoDec)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("costo_dec");

                entity.Property(e => e.DepAcumuladaVar)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("depAcumulada_var");

                entity.Property(e => e.DescripcionVar)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_var");

                entity.Property(e => e.DocumentoVar)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("documento_var");

                entity.Property(e => e.EdificioVar)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("edificio_var");

                entity.Property(e => e.EpcVar)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("epc_var");

                entity.Property(e => e.EstadoActivoVar)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estadoActivo_var");

                entity.Property(e => e.FechaAdquisicionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAdquisicion_date");

                entity.Property(e => e.FormaAdquisicionIdInt).HasColumnName("formaAdquisicionID_int");

                entity.Property(e => e.IdentificadorActivoVar)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("identificadorActivo_var");

                entity.Property(e => e.MarcaVar)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("marca_var");

                entity.Property(e => e.ModeloVar)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("modelo_var");

                entity.Property(e => e.NoSerieVar)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("noSerie_var");

                entity.Property(e => e.PisoVar)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("piso_var");

                entity.Property(e => e.UbicacionIdVar)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ubicacionID_var");

                entity.Property(e => e.ValorenLibrosVar)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("valorenLibros_var");

                entity.HasOne(d => d.CentroCostosIdVarNavigation)
                    .WithMany(p => p.Activos)
                    .HasForeignKey(d => d.CentroCostosIdVar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activos_CentroCostos");

                entity.HasOne(d => d.FormaAdquisicionIdIntNavigation)
                    .WithMany(p => p.Activos)
                    .HasForeignKey(d => d.FormaAdquisicionIdInt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activos_FormaAdquisicion");

                entity.HasOne(d => d.UbicacionIdVarNavigation)
                    .WithMany(p => p.Activos)
                    .HasForeignKey(d => d.UbicacionIdVar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activos_Ubicaciones");
            });

            modelBuilder.Entity<CentroCosto>(entity =>
            {
                entity.HasKey(e => e.CentroCostosIdVar)
                    .HasName("PK_TBLCENTRO_DE_COSTOS");

                entity.Property(e => e.CentroCostosIdVar)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("centroCostosID_var");

                entity.Property(e => e.ActivoBit).HasColumnName("activo_bit");

                entity.Property(e => e.DescripcionVar)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_var");

                entity.Property(e => e.FechaAltaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta_date");

                entity.Property(e => e.FechaModDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaMod_date");

                entity.Property(e => e.UsuarioIdInt).HasColumnName("usuarioID_int");

                entity.Property(e => e.UsuarioIdModInt).HasColumnName("usuarioID_mod_int");
            });

            modelBuilder.Entity<EpcProductosRel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("EpcProductosRel");

                entity.Property(e => e.EpcVar)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("epc_var")
                    .IsFixedLength(true);

                entity.Property(e => e.ProductoIdVar)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("productoID_var");
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.EventoIdInt)
                    .HasName("PK_Events");

                entity.Property(e => e.EventoIdInt).HasColumnName("eventoID_int");

                entity.Property(e => e.ContadorInt).HasColumnName("contador_int");

                entity.Property(e => e.EpcVar)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("epc_var");

                entity.Property(e => e.FastIdInt).HasColumnName("fastID_int");

                entity.Property(e => e.FechaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_date");

                entity.Property(e => e.LectorIdInt).HasColumnName("lectorID_int");

                entity.Property(e => e.PeakInt).HasColumnName("peak_int");

                entity.Property(e => e.PuertoInt).HasColumnName("puerto_int");

                entity.Property(e => e.TidVar)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tid_var");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");

                entity.Property(e => e.UserMemoryVar)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("userMemory_var");
            });

            modelBuilder.Entity<FormaAdquisicion>(entity =>
            {
                entity.HasKey(e => e.FormaAdquisicionIdInt);

                entity.ToTable("FormaAdquisicion");

                entity.Property(e => e.FormaAdquisicionIdInt).HasColumnName("formaAdquisicionID_int");

                entity.Property(e => e.ActivoBit).HasColumnName("activo_bit");

                entity.Property(e => e.DescripcionVar)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_var");

                entity.Property(e => e.FechaAltaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta_date");

                entity.Property(e => e.FechaModDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaMod_date");

                entity.Property(e => e.UsuarioIdInt).HasColumnName("usuarioID_int");

                entity.Property(e => e.UsuarioIdModInt).HasColumnName("usuarioID_mod_int");
            });

            modelBuilder.Entity<Lectore>(entity =>
            {
                entity.HasKey(e => e.LectorIdInt)
                    .HasName("PK_Readers");

                entity.Property(e => e.LectorIdInt).HasColumnName("lectorID_int");

                entity.Property(e => e.DescripcionVar)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_var");

                entity.Property(e => e.DireccionVar)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("direccion_var");

                entity.Property(e => e.FechaAltaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta_date");

                entity.Property(e => e.FechaModDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaMod_date");

                entity.Property(e => e.ModeloVar)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("modelo_var");

                entity.Property(e => e.UsuarioIdInt).HasColumnName("usuarioID_int");

                entity.Property(e => e.UsuarioIdModInt).HasColumnName("usuarioID_mod_int");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RolIdInt);

                entity.Property(e => e.RolIdInt).HasColumnName("rolID_int");

                entity.Property(e => e.ActivoBit).HasColumnName("activo_bit");

                entity.Property(e => e.DescripcionVar)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_var");

                entity.Property(e => e.FechaAltaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta_date");

                entity.Property(e => e.FechaModDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaMod_date");

                entity.Property(e => e.UsuarioIdInt).HasColumnName("usuarioID_int");

                entity.Property(e => e.UsuarioIdModInt).HasColumnName("usuarioID_mod_int");
            });

            modelBuilder.Entity<Ubicacione>(entity =>
            {
                entity.HasKey(e => e.UbicacionIdVar);

                entity.Property(e => e.UbicacionIdVar)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ubicacionID_var");

                entity.Property(e => e.ActivoBit).HasColumnName("activo_bit");

                entity.Property(e => e.DescripcionVar)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_var");

                entity.Property(e => e.FechaAltaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta_date");

                entity.Property(e => e.FechaModDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaMod_date");

                entity.Property(e => e.UsuarioIdInt).HasColumnName("usuarioID_int");

                entity.Property(e => e.UsuarioIdModInt).HasColumnName("usuarioID_mod_int");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioIdInt);

                entity.Property(e => e.UsuarioIdInt).HasColumnName("usuarioID_int");

                entity.Property(e => e.ActivoBit).HasColumnName("activo_bit");

                entity.Property(e => e.CodigoDeVerificacionVar)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigoDeVerificacion_var");

                entity.Property(e => e.CuentaVerificadaBit).HasColumnName("cuentaVerificada_bit");

                entity.Property(e => e.EsAdminBit).HasColumnName("esAdmin_bit");

                entity.Property(e => e.FechaAltaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta_date");

                entity.Property(e => e.FechaModDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaMod_date");

                entity.Property(e => e.NombreVar)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre_var");

                entity.Property(e => e.PasswordEncryptByte).HasColumnName("password_encrypt_byte");

                entity.Property(e => e.PasswordKeyByte).HasColumnName("password_key_byte");

                entity.Property(e => e.RolIdInt).HasColumnName("rolID_int");

                entity.Property(e => e.UsuarioIdModInt).HasColumnName("usuarioID_mod_int");

                entity.Property(e => e.UsuarioRegIdInt).HasColumnName("usuarioRegID_int");

                entity.Property(e => e.UsuarioVar)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("usuario_var");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
