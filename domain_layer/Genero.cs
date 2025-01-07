using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain_layer
{
    public class Genero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int genero_id { get; set; }

        [Required]
        [StringLength(50)]
        public string genero_nombre { get; set; }
        public virtual ICollection<Persona> Personas { get; set; }

        public bool genero_activo { get; set; }

    }
}
