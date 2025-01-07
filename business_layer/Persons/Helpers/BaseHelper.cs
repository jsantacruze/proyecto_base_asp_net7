using AutoMapper;
using data_access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Persons.Helpers
{
    public class BaseHelper
    {
        protected readonly DatabaseContext _context;
        protected readonly IMapper _mapper;

        public BaseHelper(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
