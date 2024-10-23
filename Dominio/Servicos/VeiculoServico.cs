using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos
{
    public class VeiculoServico : IVeiculoServico
    {
        private readonly DbContexto _contexto;
        public VeiculoServico(DbContexto contexto)
        {
            _contexto = contexto;
        }
        public void ApagarPorId(Veiculo veiculo)
        {
           _contexto.Veiculos.Remove(veiculo);
           _contexto.SaveChanges();
    
        }

        public void Atualizar(Veiculo veiculo)
        {
            _contexto.Veiculos.Update(veiculo);
            _contexto.SaveChanges();
        }

        public Veiculo? BuscaPorId(int id)
        {
            //return _contexto.Veiculos.Find(id);
            return _contexto.Veiculos.Where(v => v.Id == id).FirstOrDefault();
        }

        public void Incluir(Veiculo veiculo)
        {
            _contexto.Veiculos.Add(veiculo);
            _contexto.SaveChanges();
        }

        public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null)
        {
           var query = _contexto.Veiculos.AsQueryable();
           if(!String.IsNullOrEmpty(nome))
           {
                //query = query.Where( v => v.Nome.ToLower().Contains(nome.ToLower()));
                query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome}%"));
           }
            int intensPorPagina = 10;
            
            if(pagina != null)
            query = query.Skip(((int)pagina - 1) * intensPorPagina).Take(intensPorPagina);          

            return query.ToList();
        }
    }
}