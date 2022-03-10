using LibraryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApi.Services
{
    public static class BookService
    {
        public static List<BookDto> Get(int index, int? count, int? authorId)
        {
            if (Data.Storage.Books == null)
                return new();

            List<BookDto> result = new List<BookDto>();
            if (authorId == null)
            {
                result = Data.Storage.Books.Skip(index).ToList();

                if (count != null)
                    result = result.Take(Convert.ToInt32(count)).ToList();
            }
            else
            {
                if (!Data.Storage.Books.Any(x => x.AuthorId == authorId))
                    return new();

                result = Data.Storage.Books.Where(x => x.AuthorId == authorId).ToList();

                result = result.Skip(index).ToList();

                if (count != null)
                    result = result.Take(Convert.ToInt32(count)).ToList();
            }

            return result;
        }

        public static BookDto Add(BookDto book)
        {
            if (!Data.Storage.Genres.Any(x => x.Id == book.Genre.Id || x.Name == book.Genre.Name))
                book.Genre.Id = GenreService.Add(book.Genre.Name).Id;

            else if (Data.Storage.Genres.Any(x => x.Id == book.Genre.Id))
                book.Genre.Name = Data.Storage.Genres.First(x => x.Id == book.Genre.Id).Name;

            else if (Data.Storage.Genres.Any(x => x.Name == book.Genre.Name))
                book.Genre.Id = Data.Storage.Genres.First(x => x.Name == book.Genre.Name).Id;

            book.Id = 1 + Data.Storage.LastBookId;
            Data.Storage.LastBookId++;
            Data.Storage.Books.Add(book);
            return book;
        }

        public static void Delete(int id)
        {
            if (Data.Storage.Cards.Any(x => x.Book.Id == id))
                throw new Exceptions.EntityLinkedException("This book is not in the library! Delete library card first!");
            Data.Storage.Books.Remove(Data.Storage.Books.First(x => x.Id == id));
        }
    }
}
