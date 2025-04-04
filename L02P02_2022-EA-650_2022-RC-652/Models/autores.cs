using System.ComponentModel.DataAnnotations;

namespace L02P02_2022_EA_650_2022_RC_652.Models
{
    public class autores
    {
        [Key]
        public int id { get; set; }
        public string? autor { get; set; }
    }
}
