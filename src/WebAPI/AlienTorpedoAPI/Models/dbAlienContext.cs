using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AlienTorpedoAPI.Models
{
    public partial class dbAlienContext : DbContext
    {
        public dbAlienContext(DbContextOptions<dbAlienContext> options) : base(options)
        { }

        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<GrupoEvento> GrupoEvento { get; set; }
        public virtual DbSet<GrupoUsuario> GrupoUsuario { get; set; }
        public virtual DbSet<TipoEvento> TipoEvento { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<EventoSorteado> EventoSorteado { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.CdEvento)
                    .HasName("pk_Evento");

                entity.Property(e => e.CdEvento)
                    .HasColumnName("CdEvento");

                entity.Property(e => e.CdTipoEvento).HasColumnName("CdTipoEvento");
                
                entity.Property(e => e.DvParticular).HasColumnName("DvParticular");

                entity.Property(e => e.NmEndereco)
                    .HasColumnName("NmEndereco")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NmEvento)
                    .HasColumnName("NmEvento")
                    .HasColumnType("varchar(60)");

                entity.Property(e => e.VlEvento).HasColumnName("VlEvento");

                entity.Property(e => e.VlNota).HasColumnName("VlNota");

                entity.HasOne(d => d.CdTipoEventoNavigation)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.CdTipoEvento)
                    .HasConstraintName("fk_Evento_TipoEvento");
                
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasKey(e => e.CdGrupo)
                    .HasName("pk_Grupo");

                entity.Property(e => e.CdGrupo)
                    .HasColumnName("CdGrupo");

                entity.Property(e => e.DtInclusao)
                    .HasColumnName("DtInclusao")
                    .HasColumnType("datetime");

                entity.Property(e => e.NmGrupo)
                    .HasColumnName("NmGrupo")
                    .HasColumnType("varchar(60)");
            });

            modelBuilder.Entity<GrupoEvento>(entity =>
            {
                entity.HasKey(e => e.IdGrupoEvento)
                    .HasName("pk_GrupoEvento");

                entity.ToTable("GrupoEvento");

                entity.Property(e => e.IdGrupoEvento)
                    .HasColumnName("IdGrupoEvento");

                entity.Property(e => e.CdGrupo).HasColumnName("CdGrupo");

                entity.Property(e => e.CdEvento).HasColumnName("CdEvento");

                entity.Property(e => e.DtCadastro)
                    .HasColumnName("DtCadastro")
                    .HasColumnType("datetime");

                entity.Property(e => e.DtInicio)
                    .HasColumnName("DtInicio")
                    .HasColumnType("datetime");

                entity.Property(e => e.NmDescricao)
                    .HasColumnName("NmDescricao")
                    .HasColumnType("varchar(80)");

                entity.Property(e => e.DvRecorrente)
                    .HasColumnName("DvRecorrente")
                    .HasColumnType("bit");

                entity.Property(e => e.VlRecorrencia)
                    .HasColumnName("VlRecorrencia");

                entity.Property(e => e.VlDiasRecorrencia)
                    .HasColumnName("VlDiasRecorrencia");

                entity.HasOne(d => d.CdGrupoNavigation)
                    .WithMany(p => p.GrupoEvento)
                    .HasForeignKey(d => d.CdGrupo)
                    .HasConstraintName("fk_GrupoEvento_Grupo");

                entity.HasOne(d => d.CdEventoNavigation)
                    .WithMany(p => p.GrupoEvento)
                    .HasForeignKey(d => d.CdEvento)
                    .HasConstraintName("fk_GrupoEvento_Evento");

            });

            modelBuilder.Entity<GrupoUsuario>(entity =>
            {
                entity.HasKey(e => new { e.CdUsuario, e.CdGrupo })
                    .HasName("pk_GrupoUsuario");

                entity.ToTable("GrupoUsuario");

                entity.Property(e => e.CdUsuario).HasColumnName("CdUsuario");

                entity.Property(e => e.CdGrupo).HasColumnName("CdGrupo");

                entity.Property(e => e.NrVoto).HasColumnName("NrVoto");

                entity.HasOne(d => d.CdGrupoNavigation)
                    .WithMany(p => p.GrupoUsuario)
                    .HasForeignKey(d => d.CdGrupo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_GrupoUsuario_Grupo");

                entity.HasOne(d => d.CdUsuarioNavigation)
                    .WithMany(p => p.GrupoUsuario)
                    .HasForeignKey(d => d.CdUsuario)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_GrupoUsuario_Usuario");
            });

            modelBuilder.Entity<TipoEvento>(entity =>
            {
                entity.HasKey(e => e.CdTipoEvento)
                    .HasName("pk_TipoEvento");

                entity.ToTable("TipoEvento");

                entity.Property(e => e.CdTipoEvento)
                    .HasColumnName("CdTipoEvento");

                entity.Property(e => e.NmTipoEvento)
                    .HasColumnName("NmTipoEvento")
                    .HasColumnType("varchar(60)");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.CdUsuario)
                    .HasName("pk_Usuario");

                entity.Property(e => e.CdUsuario).HasColumnName("CdUsuario");

                entity.Property(e => e.DtInclusao)
                    .HasColumnName("DtInclusao")
                    .HasColumnType("datetime");

                entity.Property(e => e.DvAtivo).HasColumnName("DvAtivo");

                entity.Property(e => e.NmEmail)
                    .HasColumnName("NmEmail")
                    .HasColumnType("varchar(80)");

                entity.Property(e => e.NmSenha)
                    .HasColumnName("NmSenha")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.NmUsuario)
                    .HasColumnName("NmUsuario")
                    .HasColumnType("varchar(80)");
            });

            modelBuilder.Entity<EventoSorteado>(entity =>
            {
                entity.HasKey(e => e.IdEventoSorteado)
                    .HasName("IdEventoSorteado");

                entity.ToTable("EventoSorteado");

                entity.Property(e => e.IdEventoSorteado)
                    .HasColumnName("IdEventoSorteado");

                entity.Property(e => e.IdGrupoEvento)
                    .HasColumnName("IdGrupoEvento");

                entity.Property(e => e.DtEvento)
                    .HasColumnName("DtEvento")
                    .HasColumnType("datetime");
            });
        }
    }
}