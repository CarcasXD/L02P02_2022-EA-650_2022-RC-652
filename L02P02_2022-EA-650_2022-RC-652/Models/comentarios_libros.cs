using System.ComponentModel.DataAnnotations;

namespace L02P02_2022_EA_650_2022_RC_652.Models
{
    public class comentarios_libros
    {
        [Key]
        public int id { get; set; }
        public int? id_libro { get; set; }
        public string? comentarios {  get; set; }

    }
}
