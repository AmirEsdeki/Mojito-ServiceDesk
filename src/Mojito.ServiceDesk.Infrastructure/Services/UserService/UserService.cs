using AutoMapper;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs.User;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Services.BaseService;

namespace Mojito.ServiceDesk.Infrastructure.Services.UserService
{
    public class UserService : BaseService<User, UserDTOIn, UserDTOOut>
    {
        #region Ctor
        public UserService(ApplicationDBContext db, IMapper mapper)
            : base(db, mapper)
        {
        }
        #endregion
    }
}
