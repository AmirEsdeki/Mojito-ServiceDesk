
namespace Mojito.ServiceDesk.Core.Common.Interfaces
{
    //some of entities has different id types like IdentityUser that has its own Id,
    //so I decided to seprate them according to their interface. 
    public interface IBaseEntity : ICoreBaseEntity
    {
        int Id { get; set; }
    }

    public interface IBaseEntityWithLongId : ICoreBaseEntity
    {
        long Id { get; set; }
    }

    public interface IBaseEntityWithGuidId : ICoreBaseEntity
    {
        string Id { get; set; }
    }
}
