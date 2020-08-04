using AlienTorpedoSite.Models.Conta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlienTorpedoSite.Models.Utilidades
{
    public class Retorno
    {
        public int cdretorno { get; set; }
        public string mensagem { get; set; }
        public Usuario usuario { get; set; }
    }
}
