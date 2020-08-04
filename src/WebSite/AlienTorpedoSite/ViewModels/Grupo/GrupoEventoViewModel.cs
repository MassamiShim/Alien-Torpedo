using AlienTorpedoSite.Models.Grupo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlienTorpedoSite.ViewModels.Grupo
{
    public class GrupoEventoViewModel
    {
        public int cdretorno { get; set; }
        public string mensagem { get; set; }
        public List<GrupoEvento>data {get;set;}
    }
}
