using System;
using System.Linq;

namespace LibraryApi.Services
{
    public static class GenreService
    {
        public static Models.GenreDto Add(string name)
        {
            Models.GenreDto g = new()
            {
                Name = name,
                Id = 1 + Data.Storage.LastGenreId
            };
            Data.Storage.LastGenreId++;
            Data.Storage.Genres.Add(g);
            return g;
        }
    }
}
