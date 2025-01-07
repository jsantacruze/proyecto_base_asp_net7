using AutoMapper;
using business_layer.Persons.DTO;
using domain_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genero, GeneroDTO>();
            CreateMap<Persona, PersonaDTO>();
        }
    }
}
