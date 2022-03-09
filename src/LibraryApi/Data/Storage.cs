﻿using System;
using System.Collections.Generic;

namespace LibraryApi.Data
{
    public static class Storage
    {
        /// <summary>
        /// 1.2.3 - Books
        /// </summary>
        public static List<Models.BookDto> Books { get; set; }
        public static List<Models.GenreDto> Genres { get; set; }
        /// <summary>
        /// 1.2.3 - Humans
        /// </summary>
        public static List<Models.HumanDto> Humans { get; set; }

        /// <summary>
        /// 2.1.2
        /// </summary>
        public static List<Models.LibraryCard> Cards { get; set; }

        public static int LastBookId;
        public static int LastHumanId;
        public static int LastGenreId;

        static Storage()
        {
            Humans = new List<Models.HumanDto>
            {
                new() { Id = 1, Name = "Иван", Surname = "Иванов", Patronymic = "Иванович", Birthday = DateTime.Parse("01.01.2001")},
                new() { Id = 2, Name = "Петр", Surname = "Петров", Patronymic = "Петрович", Birthday = DateTime.Parse("02.02.2002")},
                new() { Id = 3, Name = "Богдан", Surname = "Богданов", Patronymic = "Богданович", Birthday = DateTime.Parse("03.03.2003")},
                new() { Id = 4, Name = "Александр", Surname = "Александров", Patronymic = "Александрович", Birthday = DateTime.Parse("03.03.1903")},
            };
            LastHumanId = 4;
            Genres = new List<Models.GenreDto>
            {
                new() { Id = 1, Name = "Фантастика"},
                new() { Id = 2, Name = "Детектив"},
                new() { Id = 3, Name = "Классика"}
            };
            LastGenreId = 3;
            Books = new List<Models.BookDto>
            {
                new() { Id = 1, Title = "Фантастическая фантастика I", Genre = Genres[0], AuthorId = Humans[0].Id },
                new() { Id = 2, Title = "Фантастическая фантастика II", Genre = Genres[0], AuthorId = Humans[0].Id },
                new() { Id = 3, Title = "Детективный детектив", Genre = Genres[1], AuthorId = Humans[1].Id },
                new() { Id = 4, Title = "Классическая классика", Genre = Genres[2], AuthorId = Humans[3].Id },
            };
            LastBookId = 4;
        }
    }
}
