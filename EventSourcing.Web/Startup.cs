using System.Collections.Generic;
using EventSourcing.Web.Clients.Domain.Clients;
using EventSourcing.Web.Clients.Domain.Clients.Handlers;
using EventSourcing.Web.Clients.Storage;
using EventSourcing.Web.Clients.Views.Clients;
using EventSourcing.Web.ClientsContracts.Commands;
using EventSourcing.Web.ClientsContracts.Events;
using EventSourcing.Web.ClientsContracts.Queries;
using EventSourcing.Web.Domain.Commands;
using EventSourcing.Web.Domain.Events;
using EventSourcing.Web.Domain.Queries;
using EventSourcing.Web.Storage;
using EventSourcing.Web.Transactions.Domain.Accounts;
using EventSourcing.Web.Transactions.Domain.Accounts.Handlers;
using EventSourcing.Web.Transactions.Domain.Clients.Handlers;
using EventSourcing.Web.Transactions.Views.Accounts.Handlers;
using EventSourcing.Web.TransactionsContracts.Accounts.Commands;
using EventSourcing.Web.TransactionsContracts.Accounts.Queries;
using EventSourcing.Web.TransactionsContracts.Accounts.ValueObjects;
using EventSourcing.Web.TransactionsContracts.Transactions.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace EventSourcing.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedRedisCache(x =>
            {
                x.Configuration = "localhost:6379";
            });
            ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect("localhost:6379");
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            services.AddSession();
            services.AddMvc();

            services.AddTransient<IAccountNumberGenerator, RandomAccountNumberGenerator>();

            ConfigureMediator(services);

            ConfigureCQRS(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseMvc();
        }

        private static void ConfigureCQRS(IServiceCollection services)
        {
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<IEventBus, EventBus>();

            services.AddScoped<IRequestHandler<CreateNewAccount>, CreateNewAccountHandler>();
            services.AddScoped<IRequestHandler<MakeTransfer>, ProcessInTransactionHandler>();
            services.AddScoped<IRequestHandler<GetAccounts, IEnumerable<AccountSummary>>, GetAccountsHandler>();
            services.AddScoped<IRequestHandler<GetAccount, AccountSummary>, GetAccountHandler>();

            services.AddScoped<INotificationHandler<ClientCreatedEvent>, ClientDetailView>();

            services.AddScoped<IRequestHandler<CreateClient>, ClientsCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateClient>, ClientsCommandHandler>();
            services.AddScoped<IRequestHandler<GetClients, List<Client>>, ClientsQueryHandler>();
            services.AddScoped<IRequestHandler<GetClient, Client>, ClientsQueryHandler>();
            services.AddScoped<ISession, Session>();
            services.AddSingleton<IEventStore, InMemoryEventStore>();
            services.AddScoped<IRepository, Repository>();
        }

        private static void ConfigureMediator(IServiceCollection services)
        {
            //services.AddScoped<IMediator, Mediator>();
            services.AddMediatR();
            services.AddTransient<SingleInstanceFactory>(sp => t => sp.GetService(t));
            services.AddTransient<MultiInstanceFactory>(sp => t => sp.GetServices(t));
        }
    }
}
