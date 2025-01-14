using domain_layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Persons.DTO
{
    public class PersonaDTO
    {
        public long persona_id { get; set; }

        public string persona_nombre { get; set; }

        public string persona_apellidos { get; set; }

        public string? persona_email { get; set; }

        public DateTime persona_fecha_nacimiento { get; set; }
        public string? persona_observaciones { get; set; }
        public int genero_id { get; set; }
        public GeneroDTO Genero { get; set; }

        public int estado_civil_id {get; set;}
        public EstadoCivilDTO EstadoCivil { get; set; }

    }
}
