using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace labs5Prog.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Bank { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal buy { get; set; }
        public decimal sell { get; set; }
        
        [System.ComponentModel.DataAnnotations.Schema.NotMapped] // несопоставлять с таблицей 
        public List<Currency> Currenc { get; set; } = new();
        public int? CurrencyId { get; set; } // внешний ключ
        public Currency? Currency { get; set; } //навигационное свойство
    }
}
