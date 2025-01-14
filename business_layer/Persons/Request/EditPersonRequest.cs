using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Persons.Request
{
    public class EditPersonRequest
    {
        public long persona_id { get; set; }
        public string persona_nombre { get; set; }
        public string persona_apellidos { get; set; }
        public string? persona_email { get; set; }
        public DateTime persona_fecha_nacimiento { get; set; }
        public string? persona_observaciones { get; set; }
        public int genero_id { get; set; }
        public string? persona_telefono { get; set; }
        public string? persona_movil { get; set; }
    }
}
