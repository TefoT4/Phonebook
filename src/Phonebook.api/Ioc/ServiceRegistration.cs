using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Phonebook.DataAccess;
using Phonebook.Repository;
using Phonebook.Services;

namespace Phonebook.api.Ioc
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IRepository, Repository.Repository>();
            services.AddTransient<IPhonebookService, PhonebookService>();
            services.AddTransient<IPhonebookDatabase, PhonebookDatabase>();
        }
    }
}
