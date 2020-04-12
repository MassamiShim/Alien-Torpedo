using System;
using System.ComponentModel.DataAnnotations;

namespace AlienTorpedoSite.ViewModels.Grupo
{
    public class GrupoViewModel
    {
        [Key]
        public int IdGrupoEvento { get; set; }

        public int cdGrupo { get; set; }
        //public int cdEvento { get; set; }
        [Display(Name = "Data Evento")]
        public DateTime dtEvento { get; set; }
        [Display(Name = "Descrição")]
        public string nmEvento { get; set; }

        public string nmEndereco { get; set; }

        public float vlEvento{ get; set; }
        
    }
}