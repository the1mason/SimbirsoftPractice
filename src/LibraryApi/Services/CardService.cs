using LibraryApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace LibraryApi.Services
{
    public static class CardService
    {
        public static LibraryCard Add(int humanId, int bookId)
        {
            HumanDto h = Data.Storage.Humans.First(x => x.Id == humanId);
            BookDto b = Data.Storage.Books.First(x => x.Id == bookId);
            LibraryCard card = new()
            {
                Id = 1 + Data.Storage.LastCardId,
                Book = b,
                Human = h,
                DateRecieved = DateTime.UtcNow
            };
            if(Data.Storage.Cards.Any(x => x.Book.Id == bookId))
            {
                throw new Exceptions.BookTakenException($"Book {bookId} is already taken!");
            }
            Data.Storage.LastCardId++;
            Data.Storage.Cards.Add(card);
            return card;
        }
    }
}
