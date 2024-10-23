using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinimalApi.Dominio.DTOs.Enuns;

namespace MinimalApi.Dominio.ModelViews
{
    public record AdministradorModelView
    {
        public int Id {get; set;} = default!;
        public string Email {get; set;} = default!;
        public string perfil {get; set;} = default!;
    }
}