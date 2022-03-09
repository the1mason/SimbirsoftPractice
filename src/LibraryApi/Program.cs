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
                new() { Id = 1, Name = "����", Surname = "������", Patronymic = "��������", Birthday = DateTime.Parse("01.01.2001")},
                new() { Id = 2, Name = "����", Surname = "������", Patronymic = "��������", Birthday = DateTime.Parse("02.02.2002")},
                new() { Id = 3, Name = "������", Surname = "��������", Patronymic = "����������", Birthday = DateTime.Parse("03.03.2003")},
                new() { Id = 4, Name = "���������", Surname = "�����������", Patronymic = "�������������", Birthday = DateTime.Parse("03.03.1903")},
            };
            Services.BookService.Genres = new List<Models.GenreDto>()
            {
                new() { Id = 1, Name = "����������"},
                new() { Id = 2, Name = "��������"},
                new() { Id = 3, Name = "��������"}
            };
            Services.BookService.Books = new List<Models.BookDto>()
            {
                new() { Id = 1, Title = "�������������� ���������� I", Genre = Services.BookService.Genres[0], Author = Services.HumanService.Humans[0] },
                new() { Id = 2, Title = "�������������� ���������� II", Genre = Services.BookService.Genres[0], Author = Services.HumanService.Humans[0] },
                new() { Id = 3, Title = "����������� ��������", Genre = Services.BookService.Genres[1], Author = Services.HumanService.Humans[1] },
                new() { Id = 4, Title = "������������ ��������", Genre = Services.BookService.Genres[2], Author = Services.HumanService.Humans[3] },
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
