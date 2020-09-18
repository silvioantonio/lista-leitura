using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Alura.ListaLeitura.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var _repo = new LivroRepositorioCSV();

            // Objeto 'host' que ira hospedar chamadas web
            /* O WebHostBuilder e responsavel por construir um hospedeiro web
                // A implementaçao é definida pelo 'UseKestrel()', indica que iremos usar o servidor [kestrel neste link](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-3.1) como servidor http
                // Preciso dizer qual classe sera responsavel pela inicializaçao usando UseStartup<class>()
            */
            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<StartUp>()
                .Build();
            host.Run();

            //Imprime no CONSOLE
            //ImprimeLista(_repo.ParaLer);
            //ImprimeLista(_repo.Lendo);
            //ImprimeLista(_repo.Lidos);
        }

        static void ImprimeLista(ListaDeLeitura lista)
        {
            Console.WriteLine(lista);
        }
    }
}
