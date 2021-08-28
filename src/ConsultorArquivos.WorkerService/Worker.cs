using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConsultorArquivos.LeitorArquivos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsultorArquivos.WorkerService
{
    public class Worker : BackgroundService
    {
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
                FileSystemWatcher watcher = new FileSystemWatcher();

                watcher.Path = @"E:\Exemplo";
                watcher.Filter = "*.txt";

                watcher.NotifyFilter = NotifyFilters.Attributes |
                                       NotifyFilters.CreationTime |
                                       NotifyFilters.FileName |
                                       NotifyFilters.LastWrite |
                                       NotifyFilters.LastAccess |
                                       NotifyFilters.CreationTime;

                watcher.Changed += new FileSystemEventHandler(ArquivoAtualizado);
                watcher.Created += new FileSystemEventHandler(ArquivoCriado);


                watcher.EnableRaisingEvents = true;
                await Task.Delay(100000, stoppingToken);
            }
        }

        public void ArquivoAtualizado(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"O arquivo   {e.Name}    foi alterado.");
            //Lógica para verificar atualizações e atualizar BD.
        }

        public void ArquivoCriado(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"O arquivo   {e.Name}    foi criado.");

            ManipuladorTxt.LeitorTexto(e.FullPath); 
        }
    }
}
