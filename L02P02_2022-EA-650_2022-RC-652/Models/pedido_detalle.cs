using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L02P02_2022_EA_650_2022_RC_652.Models
{
    public class pedido_detalle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int? id_pedido { get; set; }
        public int? id_libro { get; set; }
        public DateTime? created_at { get; set; }
    }
}
