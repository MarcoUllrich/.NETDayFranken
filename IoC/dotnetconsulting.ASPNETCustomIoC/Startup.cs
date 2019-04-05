// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using dotnetconsulting.ServiceAndInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace dotnetconsulting.ASPNETCustomIoC
{
    public class Startup
    {
        // Nuget: Autofac.Extensions.DependencyInjection
        // Nuget: Autofac

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Andere Framework-Services hinzufügen
            // services.AddMvc();

            // Autofac hinzufügen und konfigurieren
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DefaultModule>();

            containerBuilder.Populate(services);
            IContainer container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }

        public class DefaultModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<EMailOrderService>()
                    .As<IOrderService>()
                    // Konstruktor-Parameter
                    .WithParameter("SmtpHost", "127.0.0.1")
                    // Eigenschaft
                    .WithProperty("Sender", "tkansy@dotnetconsulting.eu");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // IoC verwenden
            IOrderService orderService = services.GetService<IOrderService>();
            orderService.PlaceOrder("Wattestäbchen", 10);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("IoC Demo");
            });
        }
    }
}
