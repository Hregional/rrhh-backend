using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data.Models;

namespace rrhh_backend.Data
{
    public partial class RrHhContext : DbContext
    {

        public RrHhContext(DbContextOptions<RrHhContext> options) : base(options)
        {
        }

        public virtual DbSet<RHColaborador> RHColaboradors { get; set; }

        public virtual DbSet<RHEstadoColaborador> RHEstadoColaboradors { get; set; }
        public virtual DbSet<RHEstadoCivil> RHEstadoCivils { get; set; }
        public virtual DbSet<RHHistorialDepartamento> RHHistorialDepartamentos { get; set; }
        public virtual DbSet<AdminBitacoraUsuario> AdminBitacoraUsuarios { get; set; }

        public virtual DbSet<AdminEstado> AdminEstados { get; set; }

        public virtual DbSet<RHDepartamento> RHDepartamento { get; set; }

        public virtual DbSet<AdminPermiso> AdminPermiso { get; set; }

        public virtual DbSet<AdminResetPassword> AdminResetPassword { get; set; }

        public virtual DbSet<AdminRole> AdminRole { get; set; }

        public virtual DbSet<AdminRolesPermiso> AdminRolesPermiso { get; set; }

        public virtual DbSet<AdminUser> AdminUser { get; set; }

        public virtual DbSet<AdminUserRole> AdminUserRole { get; set; }

        public virtual DbSet<RHTipoLicencias> RHTipoLicencias { get; set; }

        public virtual DbSet<RHEstadoLicencias> RHEstadoLicencias { get; set; }

        public virtual DbSet<RHLicencias> RHLicencias { get; set; }

        public virtual DbSet<AuditoriaEstatus> AuditoriaEstatus { get; set; }

        public virtual DbSet<RHAsueto> RHAsuetos { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=rrhh;User Id=sa;Password=yourStrong(!)Password;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RHColaborador>(entity =>
            {
                entity.HasKey(e => e.IdColaborador);
                entity.ToTable("RHColaborador");

                entity.HasOne(d => d.UserUser)
                    .WithOne(p => p.IdColaboradorNavigation)
                    .HasForeignKey<RHColaborador>(d => d.IdColaborador)
                    .HasConstraintName("FK_RHColaborador_UserUser");

                entity.HasOne(d => d.RHDepartamento)
                    .WithMany(p => p.RHColaboradores)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.Email)
                    .IsUnique()
                    .HasDatabaseName("IX_RHColaborador_Email");

                entity.HasOne(d => d.RHEstadoColaborador)
                    .WithMany(p => p.RHColaboradores)
                    .HasForeignKey(d => d.IdEstadoColaborador)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<RHHistorialDepartamento>(entity =>
            {
                entity.HasKey(e => e.IdHistorialDepartamento);

                entity.ToTable("RHHistorialDepartamento");

                entity.Property(e => e.FechaInicio)
                    .IsRequired();

                entity.Property(e => e.FechaFin);

                entity.HasOne(d => d.RHColaborador)
                    .WithMany(p => p.RHHistorialDepartamentos)
                    .HasForeignKey(d => d.IdColaborador)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RHHistorialDepartamento_RHColaborador");

                entity.HasOne(d => d.RHDepartamento)
                    .WithMany(p => p.RHHistorialDepartamentos)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RHHistorialDepartamento_RHDepartamento");
            });

            modelBuilder.Entity<RHEstadoColaborador>(entity =>
            {
                entity.HasKey(e => e.IdEstadoColaborador);

                entity.ToTable("RHEstadoColaborador");

                entity.Property(e => e.EstadosColaborador)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasMany(e => e.RHColaboradores)
                    .WithOne(c => c.RHEstadoColaborador)
                    .HasForeignKey(c => c.IdEstadoColaborador)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RHColaborador_RHEstadoColaborador");
            });

            modelBuilder.Entity<RHEstadoCivil>(entity =>
            {
                entity.HasKey(e => e.IdEstadoCivil);

                entity.ToTable("RHEstadoCivil");

                entity.Property(e => e.EstadoCivil)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.HasMany(e => e.RHColaboradores)
                    .WithOne(c => c.RHEstadoCivil)
                    .HasForeignKey(c => c.IdEstadoCivil)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RHColaborador_RHEstadoCivil");
            });

            modelBuilder.Entity<AdminBitacoraUsuario>(entity =>
            {
                entity.HasKey(e => e.IdBitacoraUser);

                entity.ToTable("AdminBitacoraUsuario");

                entity.HasIndex(e => e.IdUsuario, "IX_Relationship3");

                entity.Property(e => e.AccionBitacora)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.FechaBitacora).HasColumnType("datetime");

                entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UserBitacoraUsuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("Relationship3");
            });
            modelBuilder.Entity<AdminEstado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("AdminEstado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.NombreEstado)
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<RHDepartamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamentos);

                entity.ToTable("RHDepartamento");

                entity.Property(e => e.DescripcionDepartamento)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreDepartamento)
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<AdminPermiso>(entity =>
            {
                entity.HasKey(e => e.IdPermiso);

                entity.ToTable("AdminPermisos");

                entity.Property(e => e.DescripcionPermiso)
                    .HasMaxLength(55)
                    .IsUnicode(false);
                entity.Property(e => e.NombrePermiso)
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<AdminResetPassword>(entity =>
            {
                entity.HasKey(e => e.IdToken);

                entity.ToTable("AdminResetPassword");

                entity.HasIndex(e => e.IdUsuario, "IX_Relationship2");

                entity.Property(e => e.FechaCreacionToken).HasColumnType("datetime");
                entity.Property(e => e.FechaExpiracionToken).HasColumnType("datetime");
                entity.Property(e => e.NombreToken)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UserResetPasswords)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("Relationship2");
            });
            modelBuilder.Entity<AdminRole>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("AdminRoles");

                entity.Property(e => e.DescripcionRol)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.NombreRol)
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<AdminRolesPermiso>(entity =>
            {
                entity.HasKey(e => e.IdRolePermiso);

                entity.ToTable("AdminRolesPermisos");

                entity.HasIndex(e => e.IdRole, "IX_Relationship4");

                entity.HasIndex(e => e.IdPermiso, "IX_Relationship5");

                entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.UserRolesPermisos)
                    .HasForeignKey(d => d.IdPermiso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship5");

                entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.UserRolesPermisos)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship4");
            });
            modelBuilder.Entity<AdminUser>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);
                entity.ToTable("AdminUser");
                entity.HasIndex(e => e.IdEstado, "IX_Relationship1");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(55)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasMaxLength(55)
                    .IsUnicode(false);
                entity.HasOne(d => d.IdColaboradorNavigation)
                      .WithOne(p => p.UserUser)  
                      .HasForeignKey<AdminUser>(d => d.IdColaborador)
                      .HasConstraintName("FK_User_User_Gth_Colaborador");
                entity.HasOne(d => d.IdEstadoNavigation)
                      .WithMany(p => p.UserUsers) 
                      .HasForeignKey(d => d.IdEstado)
                      .HasConstraintName("Relationship1");
            });
            modelBuilder.Entity<AdminUserRole>(entity =>
            {
                entity.HasKey(e => e.IdUserRoles);

                entity.ToTable("AdminUserRoles");

                entity.HasIndex(e => e.IdUsuario, "IX_Relationship7");

                entity.HasIndex(e => e.IdRole, "IX_Relationship8");

                entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");

                entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.UserUserRoles)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship8");

                entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UserUserRoles)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship7");
            });

            modelBuilder.Entity<RHLicencias>(entity =>
            {
                entity.HasKey(e => e.IdLicencias);
                entity.ToTable("RHLicencias");

                entity.HasIndex(e => e.Uuid).IsUnique();

                entity.HasOne(d => d.RHColaborador)
                    .WithMany(p => p.RHLicencias)
                    .HasForeignKey(d => d.IdColaborador)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.RHTipoLicencias)
                    .WithMany(p => p.RHLicencias)
                    .HasForeignKey(d => d.IdTipoLicencia)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.RHEstadoLicencias)
                    .WithMany(p => p.RHLicencias)
                    .HasForeignKey(d => d.IdEstadoLicencia)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RHTipoLicencias>(entity =>
            {
                entity.HasKey(e => e.IdTipoLicencia);
                entity.ToTable("RHTipoLicencias");
            });

            modelBuilder.Entity<RHEstadoLicencias>(entity =>
            {
                entity.HasKey(e => e.IdEstadoLicencia);
                entity.ToTable("RHEstadoLicencias");
            });

            modelBuilder.Entity<AuditoriaEstatus>(entity =>
            {
                entity.HasOne(d => d.Trabajador)
                    .WithMany() // Assuming RHColaborador doesn't have a navigation property back to AuditoriaEstatus
                    .HasForeignKey(d => d.TrabajadorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AuditoriaEstatus_RHColaborador");

                entity.HasOne(d => d.EstatusAnteriorNavigation)
                    .WithMany() // Assuming RHEstadoColaborador doesn't have a navigation property back to AuditoriaEstatus
                    .HasForeignKey(d => d.IdEstatusAnterior)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AuditoriaEstatus_EstatusAnterior");

                entity.HasOne(d => d.EstatusNuevoNavigation)
                    .WithMany() // Assuming RHEstadoColaborador doesn't have a navigation property back to AuditoriaEstatus
                    .HasForeignKey(d => d.IdEstatusNuevo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AuditoriaEstatus_EstatusNuevo");
            });

            modelBuilder.Entity<RHAsueto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("RHAsueto");
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
