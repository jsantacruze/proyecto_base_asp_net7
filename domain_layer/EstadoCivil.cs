using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain_layer
{
    public class EstadoCivil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int estado_civil_id { get; set;}

        [Required]
        [StringLength(50)]
        public string estado_descripcion { get; set; }

        [Required]
        public bool estado_activo { get; set; }
    }
}
