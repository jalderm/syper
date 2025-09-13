using AutoMapper;
using Syper.Clients;
using Syper.Coaching;

namespace Syper;

public class SyperApplicationAutoMapperProfile : Profile
{
    public SyperApplicationAutoMapperProfile()
    {
        CreateMap<Client, ClientDto>();
        CreateMap<CreateUpdateClientDto, Client>();
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
