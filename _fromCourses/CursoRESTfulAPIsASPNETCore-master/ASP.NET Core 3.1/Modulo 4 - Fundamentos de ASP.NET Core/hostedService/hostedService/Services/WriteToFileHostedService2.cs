﻿using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace hostedService.Services
{
    public class WriteToFileHostedService2 : IHostedService, IDisposable
    {
        private readonly IHostEnvironment environment;
        private readonly string fileName = "Archivo 2.txt";
        private Timer timer;

        public WriteToFileHostedService2(IHostEnvironment environment)
        {
            this.environment = environment;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            WriteToFile($"WriteToFileHostedService2: Proceso iniciado {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(8));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            WriteToFile($"WriteToFileHostedService2: Proceso detenido {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            WriteToFile($"WriteToFileHostedService2: Hacer una operación {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
        }

        private void WriteToFile(string message)
        {
            string path = $@"{environment.ContentRootPath}\wwwroot\{fileName}";
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(message);
            }
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
