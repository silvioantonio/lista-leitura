using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    public class LivrosController
    {

        
        //O compilador ignora os modificadores na avaliação de um delegate.
        //Quando uma requisição for feita, um obj do HttpContext vira
        //O Retorno deve ser Task (Paralelismo), pois o metodo 'Run()' aceita apenas esse tipo
        //O WriteAsync() retorna o tipo task
        public static Task ParaLer(HttpContext httpContext)
        {
            var _repo = new LivroRepositorioCSV();

            var carregarArquivo = HtmlUtils.CarregaArquivoHtml("para-ler.html");

            foreach (var item in _repo.ParaLer.Livros)
            {
                carregarArquivo = carregarArquivo.Replace("#item#", $"<li>{item.Titulo} - {item.Autor}</li>#item#");
            }
            carregarArquivo = carregarArquivo.Replace("#item#", "");

            return httpContext.Response.WriteAsync(carregarArquivo);
        }

        public static Task Lendo(HttpContext httpContext)
        {
            var _repo = new LivroRepositorioCSV();
            return httpContext.Response.WriteAsync(_repo.Lendo.ToString());
        }

        public static Task Lidos(HttpContext httpContext)
        {
            var _repo = new LivroRepositorioCSV();
            return httpContext.Response.WriteAsync(_repo.Lidos.ToString());
        }

        private Task TesteHeader(HttpContext httpContext)
        {
            var variavel = httpContext.Request.Method;
            return httpContext.Response.WriteAsync($"Metodo utilizado nessa requisicao : {variavel}");
        }

        public static Task Detalhes(HttpContext context)
        {
            var id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);

            return context.Response.WriteAsync(livro.Detalhes());
        }

    }
}
