using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos
{
    public class AdministradorServico : IAdministradorServico
    {
        private readonly DbContexto _contexto;  
        public AdministradorServico (DbContexto contexto )
        {
            _contexto = contexto;
        }
        
        public Administrador? BuscaPorId(int id)
        {
            return _contexto.administradores.Where(v => v.Id == id).FirstOrDefault();
        }
                    
        public Administrador Incluir(Administrador administrador)
        {
    
            _contexto.administradores.Add(administrador);
            _contexto.SaveChanges();
            return administrador;
          
        }

        public Administrador? Login(LoginDTO loginDTO)
        {
            var adm = _contexto.administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
            return adm;
        }

        public List<Administrador>? Todos(int? pagina )
        {   
            var query = _contexto.administradores.AsQueryable();
           
            int itensPorPagina =10;

            if(pagina != null){
                query = query.Skip(((int)pagina -1) * itensPorPagina).Take(itensPorPagina);
            }

            return query.ToList();
        }
        
    }
}