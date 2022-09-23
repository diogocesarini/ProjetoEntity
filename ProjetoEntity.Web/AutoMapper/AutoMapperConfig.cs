using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProvaCandidato.AutoMapper
{

    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            var _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ViewModelToDataTransferObjectMappingProfile());
            });
        }
    }
}