using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    public class LivrosController : Controller
    {

        public IEnumerable<Livro> Livros { get; set; }

        //O compilador ignora os modificadores na avaliação de um delegate.
        //Quando uma requisição for feita, um obj do HttpContext vira
        //O Retorno deve ser Task (Paralelismo), pois o metodo 'Run()' aceita apenas esse tipo
        //O WriteAsync() retorna o tipo task
        public IActionResult ParaLer()
        {
            var _repo = new LivroRepositorioCSV();

            //var carregarArquivo = HtmlUtils.CarregaArquivoHtml("para-ler.html");

            //foreach (var item in _repo.ParaLer.Livros)
            //{
            //    carregarArquivo = carregarArquivo.Replace("#item#", $"<li>{item.Titulo} - {item.Autor}</li>#item#");
            //}
            //carregarArquivo = carregarArquivo.Replace("#item#", "");

            //return httpContext.Response.WriteAsync(carregarArquivo);

            ViewBag.Livros = _repo.ParaLer.Livros;

            //var html = new ViewResult { ViewName = "lista" };

            return View("lista");
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

        public string Detalhes(int id)
        {
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);

            return livro.Detalhes();
        }

    }
}
