using System;
/// <summary>
/// 2.1
/// </summary>
namespace LibraryApi.Models
{
    public class HumanDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
    }
}
