using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain_layer
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long persona_id { get; set; }

        [Required]
        [StringLength(80)]
        public string persona_nombre { get; set; }

        [Required]
        [StringLength(80)]
        public string persona_apellidos { get; set; }

        [StringLength(80)]
        public string? persona_email { get; set; }


        [Required]
        public DateTime persona_fecha_nacimiento { get; set; }
        [StringLength(255)]
        public string? persona_observaciones { get; set; }

        [Required]
        public int genero_id { get; set; }
        [ForeignKey("genero_id")]
        public virtual Genero Genero { get; set; }

        [StringLength(50)]
        public string? persona_telefono { get; set; }
        [StringLength(50)]
        public string? persona_movil { get; set; }


    }
}
