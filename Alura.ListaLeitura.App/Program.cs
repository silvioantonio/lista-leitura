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
                // A implementaçao é definida pelo 'UseKestrel()', indica qual servidor que implementa o modelo http
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
