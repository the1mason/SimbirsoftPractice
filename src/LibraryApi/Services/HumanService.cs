using LibraryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApi.Services
{
    public static class HumanService
    {
        public static List<HumanDto> Get(int index, int? count, bool authorsOnly, string query)
        {
            if (Data.Storage.Humans == null)
                return new();

            List<HumanDto> result = new List<HumanDto>();
            if (query == null)
            {
                result = Data.Storage.Humans.Skip(index).ToList();

                if (count != null)
                    result = result.Take(Convert.ToInt32(count)).ToList();
            }
            else
            {
                query = query.ToUpper();
                result.AddRange(Data.Storage.Humans.Where(x => x.Name.ToUpper().Contains(query) || x.Surname.ToUpper().Contains(query) || x.Patronymic.ToUpper().Contains(query)));
                result = result.Distinct().Skip(index).ToList();

                if (count != null)
                    result = result.Take(Convert.ToInt32(count)).ToList();
            }

            if (authorsOnly)
            {
                if (Data.Storage.Books == null)
                    return new();

                result = result.Where(x => Data.Storage.Books.Any(y => y.AuthorId == x.Id)).ToList();
            }
            return result;
        }

        public static HumanDto Add(HumanDto human)
        {
            human.Id = 1 + Data.Storage.LastHumanId;
            Data.Storage.LastHumanId++;
            Data.Storage.Humans.Add(human);
            return human;
        }

        public static void Delete(int id)
        {
            if (Data.Storage.Books.Any(x => x.AuthorId == id))
                throw new Exceptions.EntityLinkedException("This human is an author with books! Delete books first.");
            if (Data.Storage.Cards.Any(x => x.Human.Id == id))
                throw new Exceptions.EntityLinkedException("This human has library card(s). Delete cards first.");
            Data.Storage.Humans.Remove(Data.Storage.Humans.First(x => x.Id == id));
        }
    }
}
