using AutoMapper;
using Mojito.ServiceDesk.Infrastructure.Data.EF;

namespace Mojito.ServiceDesk.Infrastructure.Services.UserService
{
    public class UserService
    {
        #region Ctor
        public UserService(ApplicationDBContext db, IMapper mapper)
        {
        }
        #endregion
    }
}
