using System.ComponentModel.DataAnnotations;

namespace L02P02_2022_EA_650_2022_RC_652.Models
{
    public class clientes
    {
        [Key]
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? email { get; set; }
        public string? direccion { get; set; }
        public DateTime? created_at { get; set; }
    }
}
