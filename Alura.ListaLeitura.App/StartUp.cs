using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class StartUp
    {
        //Passando minha interface por parametro no metodo, o aspnet ira instanciar automaticamente(similar ao @Autowired no spring)
        public void ConfigureServices(IServiceCollection service)
        { 
            service.AddRouting();
        }

       //public void Configure(IApplicationBuilder applicationBuilder) 
       // {
       //     //Um objeto desta classe é passado como argumento de entrada do delegate RequestDelegate para escrever as respostas das requisições.
       //     applicationBuilder.Run(Roteamento);
       // }

        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("livros/paraler", LivrosParaLer);
            builder.MapRoute("livros/lidos", LivrosLidos);
            builder.MapRoute("livros/lendo", LivrosLendo);

            //Colocando essa constraint ({id:int}), eu delimito o id apenas ao tipo inteiro, caso contrario erro 404
            builder.MapRoute("livros/detalhes/{id:int}", ExibirDetalhesLivro);

            builder.MapRoute("cadastro/novolivro/{nome}/{autor}", NovoLivro);
            builder.MapRoute("cadastro/incluir", IncluirLivro);
            builder.MapRoute("cadastro/novolivro", Exibeformulario);

            var rotas = builder.Build();

            app.UseRouter(rotas);
        }

        private Task Exibeformulario(HttpContext context)
        {
            var html = @"
                        <html>
                            <form action='incluir'>
                                <input name='titulo'/>
                                <input name='autor'/>
                                <button>Cadastrar</button>
                            </form>
                        </html>";
            return context.Response.WriteAsync(html);
        }

        // Roteamento(antigo) feito manualmente sem o ASPNET.CORE
        private Task Roteamento(HttpContext httpContext)
        {
            var _repo = new LivroRepositorioCSV();

            var caminhosAtendidos = new Dictionary<string, RequestDelegate>
            {
                {"/livros/paraler", LivrosParaLer },
                {"/livros/lendo", LivrosLendo },
                {"/livros/lidos", LivrosLidos },
                {"/", TesteHeader }
            };

            if (caminhosAtendidos.ContainsKey(httpContext.Request.Path))
            {
                var metodo = caminhosAtendidos[httpContext.Request.Path];
                return metodo.Invoke(httpContext);
            }

            httpContext.Response.StatusCode = 404;
            return httpContext.Response.WriteAsync("Pagina inexistente!");
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

        private Task LivrosLendo(HttpContext httpContext)
        {
            var _repo = new LivroRepositorioCSV();
            return httpContext.Response.WriteAsync(_repo.Lendo.ToString());
        }

        private Task LivrosLidos(HttpContext httpContext)
        {
            var _repo = new LivroRepositorioCSV();
            return httpContext.Response.WriteAsync(_repo.Lidos.ToString());
        }

        private Task TesteHeader(HttpContext httpContext)
        {
            var variavel = httpContext.Request.Method;
            return httpContext.Response.WriteAsync($"Metodo utilizado nessa requisicao : {variavel}");
        }

        private Task NovoLivro(HttpContext context)
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

        private Task IncluirLivro(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = context.Request.Query["titulo"].ToString(),
                Autor = context.Request.Query["autor"].ToString()
            };
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi adicionado!!!");
        }

        private Task ExibirDetalhesLivro(HttpContext context)
        {
            var id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);

            return context.Response.WriteAsync(livro.Detalhes());
        }
    }
}