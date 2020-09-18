using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.ListaLeitura.App
{
    public class StartUp
    {
        //Passando minha interface por parametro no metodo, o aspnet ira instanciar automaticamente(similar ao @Autowired no spring)
        public void ConfigureServices(IServiceCollection service)
        { 
            //service.AddRouting();// Adiciona roteamento
            service.AddMvc();// Adiciona o Mvc, internamente ja chama o addRouting()
        }

       //public void Configure(IApplicationBuilder applicationBuilder) 
       // {
       //     //Um objeto desta classe é passado como argumento de entrada do delegate RequestDelegate para escrever as respostas das requisições.
       //     applicationBuilder.Run(Roteamento);
       // }

        public void Configure(IApplicationBuilder app)
        {
            /*
             * Isso era usado antes do mvc
             * 
                    var builder = new RouteBuilder(app);

                    //Agora aplicamos um roteamento padrao criado com base no padrao das classes e metodos usando reflection
                    builder.MapRoute("{classe}/{metodo}", RoteamentoPadrao.TratamentoPadrao);

                    //builder.MapRoute("livros/ParaLer", LivrosLogica.ParaLer);
                    //builder.MapRoute("livros/Lidos", LivrosLogica.Lidos);
                    //builder.MapRoute("livros/Lendo", LivrosLogica.Lendo);

                    ////Colocando essa constraint ({id:int}), eu delimito o id apenas ao tipo inteiro, caso contrario erro 404
                    //builder.MapRoute("livros/Detalhes/{id:int}", LivrosLogica.Detalhes);

                    //builder.MapRoute("cadastro/NovoLivro/{nome}/{autor}", CadastroLogica.NovoLivro);
                    //builder.MapRoute("cadastro/Incluir", CadastroLogica.Incluir);
                    //builder.MapRoute("cadastro/Exibir", CadastroLogica.Exibir);

                    var rotas = builder.Build();

                    app.UseRouter(rotas);
            */

            //Utilizando essa funcionalidade apenas em ambiente de desenvolvimento
            app.UseDeveloperExceptionPage();

            // Com mvc o padrao no nome das classes deve ser seguido
            app.UseMvcWithDefaultRoute();

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