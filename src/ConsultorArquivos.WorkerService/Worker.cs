using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConsultorArquivos.Domain.Models;
using ConsultorArquivos.LeitorArquivos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsultorArquivos.WorkerService
{
    public class Worker : BackgroundService
    {
        //Serve apenas para mostrar um log, uma mensagem.
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //Objeto que irá consultar se ha alterações na pasta/documentos.
                using var watcher = new FileSystemWatcher(@"E:\Exemplo");

                watcher.Filter = "*.txt";

                watcher.NotifyFilter = NotifyFilters.Attributes |
                                       NotifyFilters.CreationTime |
                                       NotifyFilters.FileName |
                                       NotifyFilters.LastWrite |
                                       NotifyFilters.LastAccess |
                                       NotifyFilters.CreationTime;

                watcher.Changed += ArquivoAtualizado;
                watcher.Created += new FileSystemEventHandler(ArquivoCriado);


                watcher.EnableRaisingEvents = true;
                
                await Task.Delay(1000, stoppingToken);

            }
        }

        
        public void ArquivoAtualizado(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            Console.WriteLine($"O arquivo   {e.Name}    foi alterado.");
            //Lógica para verificar atualizações e atualizar BD.
        }


        public async void ArquivoCriado(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"O arquivo   {e.Name}    foi criado.");

            var clientes = new List<Cliente>();

            //O LeitorTexto estava sendo chamado muito rápido, criando o problema de abrir uma 
            //verificação antes da ultima ter fechado, por isso o Sleep.
            Thread.Sleep(1000);
            clientes = await ManipuladorTxt.LeitorTexto(e.FullPath);
        }
    }
}
