using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

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
            builder.MapRoute("livros/paraler", LivrosLogica.LivrosParaLer);
            builder.MapRoute("livros/lidos", LivrosLogica.LivrosLidos);
            builder.MapRoute("livros/lendo", LivrosLogica.LivrosLendo);

            //Colocando essa constraint ({id:int}), eu delimito o id apenas ao tipo inteiro, caso contrario erro 404
            builder.MapRoute("livros/detalhes/{id:int}", LivrosLogica.ExibirDetalhesLivro);

            builder.MapRoute("cadastro/novolivro/{nome}/{autor}", CadastroLogica.NovoLivro);
            builder.MapRoute("cadastro/incluir", CadastroLogica.IncluirLivro);
            builder.MapRoute("cadastro/novolivro", CadastroLogica.Exibeformulario);

            var rotas = builder.Build();

            app.UseRouter(rotas);
        }

        

        

        // Roteamento(antigo) feito manualmente sem o ASPNET.CORE
        //private Task Roteamento(HttpContext httpContext)
        //{
        //    var _repo = new LivroRepositorioCSV();

        //    var caminhosAtendidos = new Dictionary<string, RequestDelegate>
        //    {
        //        {"/livros/paraler", LivrosParaLer },
        //        {"/livros/lendo", LivrosLendo },
        //        {"/livros/lidos", LivrosLidos },
        //        {"/", TesteHeader }
        //    };

        //    if (caminhosAtendidos.ContainsKey(httpContext.Request.Path))
        //    {
        //        var metodo = caminhosAtendidos[httpContext.Request.Path];
        //        return metodo.Invoke(httpContext);
        //    }

        //    httpContext.Response.StatusCode = 404;
        //    return httpContext.Response.WriteAsync("Pagina inexistente!");
        //}

        
    }
}