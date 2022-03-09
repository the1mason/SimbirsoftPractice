using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApi.Services
{
    public static class HumanService
    {
       public static List<Models.HumanDto> Get(int index, int? count, bool authorsOnly, string query)
       {
            if (Data.Storage.Humans == null)
                return new();

            List<Models.HumanDto> result = new List<Models.HumanDto>();
            if(query == null)
            {
                result = Data.Storage.Humans.Skip(index).ToList();

                if(count != null)
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
    }
}
