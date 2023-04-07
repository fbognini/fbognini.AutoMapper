using AutoMapper;
using System.Collections.Generic;

namespace fbognini.AutoMapper.Mappings
{
    internal class CustomMappingProfile : Profile
    {
        public CustomMappingProfile()
        {
            AllowNullCollections = true;
        }

        public CustomMappingProfile(IEnumerable<IHaveCustomMapping> haveCustomMappings)
            : this()
        {
            foreach (var item in haveCustomMappings)
                item.CreateMappings(this);
        }
    }
}
