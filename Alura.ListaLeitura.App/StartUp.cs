using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class StartUp
    {
       public void Configure(IApplicationBuilder applicationBuilder) 
        {
            //Um objeto desta classe é passado como argumento de entrada do delegate RequestDelegate para escrever as respostas das requisições.
            applicationBuilder.Run(Roteamento);
        }

        private Task Roteamento(HttpContext httpContext)
        {
            var _repo = new LivroRepositorioCSV();

            var caminhosAtendidos = new Dictionary<string, string>
            {
                {"/livros/paraler", _repo.ParaLer.ToString() },
                {"/livros/lendo", _repo.Lendo.ToString() },
                {"/livros/lidos", _repo.Lidos.ToString() }
            };

            if(caminhosAtendidos.ContainsKey(httpContext.Request.Path))
                return httpContext.Response.WriteAsync(caminhosAtendidos[httpContext.Request.Path]);

            return httpContext.Response.WriteAsync(httpContext.Request.Path);
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