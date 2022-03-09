using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //static lists
            Services.HumanService.Humans = new List<Models.HumanDto>()
            {
                new() { Id = 1, Name = "Иван", Surname = "Иванов", Patronymic = "Иванович", Birthday = DateTime.Parse("01.01.2001")},
                new() { Id = 2, Name = "Петр", Surname = "Петров", Patronymic = "Петрович", Birthday = DateTime.Parse("02.02.2002")},
                new() { Id = 3, Name = "Богдан", Surname = "Богданов", Patronymic = "Богданович", Birthday = DateTime.Parse("03.03.2003")},
                new() { Id = 4, Name = "Александр", Surname = "Александров", Patronymic = "Александрович", Birthday = DateTime.Parse("03.03.1903")},
            };
            Services.BookService.Genres = new List<Models.GenreDto>()
            {
                new() { Id = 1, Name = "Фантастика"},
                new() { Id = 2, Name = "Детектив"},
                new() { Id = 3, Name = "Классика"}
            };
            Services.BookService.Books = new List<Models.BookDto>()
            {
                new() { Id = 1, Title = "Фантастическая фантастика I", Genre = Services.BookService.Genres[0], Author = Services.HumanService.Humans[0] },
                new() { Id = 2, Title = "Фантастическая фантастика II", Genre = Services.BookService.Genres[0], Author = Services.HumanService.Humans[0] },
                new() { Id = 3, Title = "Детективный детектив", Genre = Services.BookService.Genres[1], Author = Services.HumanService.Humans[1] },
                new() { Id = 4, Title = "Классическая классика", Genre = Services.BookService.Genres[2], Author = Services.HumanService.Humans[3] },
            };
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
