using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlienTorpedoAPI.Models
{
    public partial class GrupoEvento
    {
        public GrupoEvento()
        {

        }

        public GrupoEvento(int IdGrupoEvento, int CdGrupo, string NmDescricao)
        {
            this.IdGrupoEvento = IdGrupoEvento;
            this.CdGrupo = CdGrupo;
            this.NmDescricao = NmDescricao;
        }

        public GrupoEvento(int IdGrupoEvento, int CdGrupo, string NmDescricao, DateTime DtCadastro, DateTime DtInicio, bool DvRecorrente, int? VlRecorrencia, int? VlDiasRecorrencia)
        {
            this.IdGrupoEvento = IdGrupoEvento;
            this.CdGrupo = CdGrupo;
            this.NmDescricao = NmDescricao;
            this.DtCadastro = DtCadastro;
            this.DtInicio = DtInicio;
            this.DvRecorrente = DvRecorrente;
            this.VlRecorrencia = VlRecorrencia;
            this.VlDiasRecorrencia = VlDiasRecorrencia;
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdGrupoEvento { get; set; }
        public int CdGrupo { get; set; }
        public string NmDescricao { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime DtInicio { get; set; }
        public bool DvRecorrente { get; set; }
        public int? VlRecorrencia { get; set; }
        public int? VlDiasRecorrencia { get; set; }

        public virtual Grupo CdGrupoNavigation { get; set; }
    }
}
