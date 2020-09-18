using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    public class CadastroController
    {

        public string Incluir(Livro livro)
        {
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return "O livro foi adicionado!!!";
        }

        //O estagio executeResult acontece depois da execuçao da action, e caso nao tratado, ele retorna html como texto puro
        public IActionResult Exibir(HttpContext context)
        {
            //var html = HtmlUtils.CarregaArquivoHtml("formulario.html");
            var html = new ViewResult() { ViewName = "formulrio.html" };
            return html;
        }


        /*
         * Antes do framework deveriamos escrever e declarar os metodos controladores dessa forma

        public static Task NovoLivro(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = context.GetRouteValue("titulo").ToString(),
                Autor = context.GetRouteValue("autor").ToString()
            };
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi adicionado!!!");
        }

        public static Task Incluir(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = context.Request.Form["titulo"].ToString(),
                Autor = context.Request.Form["autor"].ToString()
            };
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi adicionado!!!");
        }

        public static Task Exibir(HttpContext context)
        {
            var html = HtmlUtils.CarregaArquivoHtml("formulario.html");
            return context.Response.WriteAsync(html);
        }
        */
    }
}
