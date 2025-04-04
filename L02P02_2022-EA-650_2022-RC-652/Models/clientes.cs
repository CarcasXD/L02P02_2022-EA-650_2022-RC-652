using System.ComponentModel.DataAnnotations;

namespace L02P02_2022_EA_650_2022_RC_652.Models
{
    public class clientes
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string? nombre { get; set; }
        [Required]
        public string? apellido { get; set; }
        [Required]
        public string? email { get; set; }
        [Required]
        public string? direccion { get; set; }
        [Required]
        public DateTime? created_at { get; set; }
    }
}
