using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class StartUp
    {
       public void Configure(IApplicationBuilder applicationBuilder) 
        {
            //Um objeto desta classe é passado como argumento de entrada do delegate RequestDelegate para escrever as respostas das requisições.
            applicationBuilder.Run(LivrosParaLer);
        }

        //O compilador ignora os modificadores na avaliação de um delegate.
        //Quando uma requisição for feita, um obj do HttpContext vira
        //O Retorno deve ser Task (Paralelismo), pois o metodo 'Run()' aceita apenas esse tipo
        //O WriteAsync() retorna o tipo task
        private Task LivrosParaLer(HttpContext httpContext)
        {
            var _repo = new LivroRepositorioCSV();
            return httpContext.Response.WriteAsync(_repo.ParaLer.ToString());
        }

    }
}