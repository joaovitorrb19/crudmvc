using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Data;
using ProjetoCurso.Model;

namespace ProjetoCurso.Services {
    public class PopularViewBagListaParaProdutos {

        public static async  Task<SelectList> PopularLista([FromServices]DataContext context){
            var auxLista = await context.Produtos.ToListAsync();
                SelectList lista = new SelectList(auxLista,nameof(ProdutoModel.ProdutoId),nameof(ProdutoModel.NomeProduto));
                   
                return lista;
        } 

    }
}