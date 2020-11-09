
namespace Mojito.ServiceDesk.Core.Common.Interfaces
{
    public interface IBaseEntity : ICoreBaseEntity
    {
        //some of entities has different id types like IdentityUser that has its own Id,
        //so I decided to seprate them according to their interface. 
        int Id { get; set; }
    }
}
