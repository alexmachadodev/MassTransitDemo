using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MassTransitDemo.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<CustomerProductConsumer>();

                        //x.UsingInMemory((context, cfg) =>
                        //{
                        //    cfg.ConfigureEndpoints(context);
                        //});

                        x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cur =>
                        {
                            cur.UseHealthCheck(provider);
                            cur.Host(new Uri("rabbitmq://localhost"), h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });
                            cur.ReceiveEndpoint("customerProductEvent", oq =>
                            {
                                oq.PrefetchCount = 20;
                                oq.UseMessageRetry(r => r.Interval(2, 100));
                                oq.ConfigureConsumer<CustomerProductConsumer>(provider);
                            });
                        }));

                        //x.UsingAzureServiceBus((context, cfg) =>
                        //{
                        //    var connectionString = "your connection string";
                        //    cfg.Host(connectionString);

                        //    cfg.ConfigureEndpoints(context);
                        //});
                    });

                    services.AddMassTransitHostedService(true);

                    //services.AddHostedService<Worker>();
                });
    }
}
