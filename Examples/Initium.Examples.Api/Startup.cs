// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using HotChocolate.Types.Pagination;
using Initium.Api.Authentication.Core.Extensions;
using Initium.Api.Authentication.Core.GraphQL.EntityTypes;
using Initium.Api.Authentication.Core.SqlServer;
using Initium.Api.Authorization.Extensions;
using Initium.Api.Authorization.SqlServer;
using Initium.Api.Core.Database;
using Initium.Api.Core.GraphQL;
using Initium.Api.Core.Settings;
using Initium.Examples.Api.Infrastructure;
using Initium.Examples.Api.Infrastructure.GraphQL;
using Initium.Examples.Api.Infrastructure.GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Initium.Examples.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            if (env == null)
            {
                throw new ArgumentNullException(nameof(env));
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddJsonFile($"appsettings.{Environment.MachineName}.json", true, true);

            builder.AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DataSettings>(this.Configuration.GetSection("Data"));

            services.AddInitiumAuthentication(this.Configuration)
                .WithSqlServerStore();
            
            services.AddInitiumAuthorization(this.Configuration)
                .WithSqlServerStore();

            services.AddDbContext<GenericDataContext>((provider, builder) =>
            {
                var dataSettings = provider.GetRequiredService<IOptions<DataSettings>>();
                builder.UseSqlServer(dataSettings.Value.PrimaryConnectionString);
                builder.LogTo(Console.WriteLine);
                builder.ReplaceService<IModelCacheKeyFactory,
                    DbSchemaAwareModelCacheKeyFactory>();
            });

            services.AddGraphQLServer()
                .BindRuntimeType<Guid, GuidType>()
                .AddType<AuthenticatedReadOnlyUserInterfaceType>()
                .AddType<UserType>()
                .AddMutationType<MutationType>()
                .AddQueryType<CustomQueryType>()
                .AddType<CustomQueryTypeExtension>()
                .RegisterAuthentication()
                .RegisterAuthorization()
                .AddFiltering()
                .AddProjections()
                .AddSorting()
                .SetPagingOptions(new PagingOptions
                {
                    DefaultPageSize = 10,
                    IncludeTotalCount = true,
                    MaxPageSize = 100,
                });

            services.TryAddScoped<ISchemaIdentifier, CoreSchemaIdentifier>();

            services.AddHostedService<StartupService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseRouting()
                .UseEndpoints(endpoints => { endpoints.MapGraphQL(); });
        }
    }
}