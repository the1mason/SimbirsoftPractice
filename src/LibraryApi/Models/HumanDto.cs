using System;

namespace LibraryApi.Models
{
    /// <summary>
    /// 1.2.1
    /// </summary>
    public class HumanDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
    }
}
