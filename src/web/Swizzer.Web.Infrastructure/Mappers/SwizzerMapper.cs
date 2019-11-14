using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Mappers
{
    public interface ISwizzerMapper
    {
        TDesc MapTo<TDesc>(object o);
    }
    public class SwizzerMapper : ISwizzerMapper
    {
        private readonly IMapper _mapper;

        public SwizzerMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDesc MapTo<TDesc>(object obj)
            => _mapper.Map<TDesc>(obj);
    }
}
