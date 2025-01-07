using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Persons.DTO
{
    public class GeneroDTO
    {
        public int genero_id { get; set; }

        public string genero_nombre { get; set; }
    }
}
