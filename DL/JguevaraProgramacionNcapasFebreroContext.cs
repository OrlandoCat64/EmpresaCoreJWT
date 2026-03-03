using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class JguevaraProgramacionNcapasFebreroContext : DbContext
{
    public JguevaraProgramacionNcapasFebreroContext()
    {
    }

    public JguevaraProgramacionNcapasFebreroContext(DbContextOptions<JguevaraProgramacionNcapasFebreroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<UsuarioLogin> UsuarioLogin { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsuarioLogin>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C36FF1403");

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF971CD5336C");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D105346D50BD6F").IsUnique();

            entity.Property(e => e.ApellidoMaterno).HasMaxLength(100);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.UsuarioNombre).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(400);

            //entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
            //    .HasForeignKey(d => d.IdRol)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Usuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
