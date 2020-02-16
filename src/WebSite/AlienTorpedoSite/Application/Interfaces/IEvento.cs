using AlienTorpedoSite.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlienTorpedoSite.Application.Interfaces
{
    public interface IEvento
    {
       List<TipoEvento> ObtemTiposEvento();
    }
}
