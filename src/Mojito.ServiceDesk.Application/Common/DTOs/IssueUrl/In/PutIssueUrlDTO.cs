using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.IssueUrl.In
{
    public class PutIssueUrlDTO : BaseDTOPut, IMapFrom<Core.Entities.Ticketing.IssueUrl>
    {
        [StringLength(255)]
        public string Url { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PutIssueUrlDTO, Core.Entities.Ticketing.IssueUrl>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
