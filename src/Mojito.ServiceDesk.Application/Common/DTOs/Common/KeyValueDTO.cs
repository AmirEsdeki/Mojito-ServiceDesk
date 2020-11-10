namespace Mojito.ServiceDesk.Application.Common.DTOs.Common
{
    public class KeyValueDTO
    {
        public KeyValueDTO(int id, string value)
        {
            Id = id;
            Value = value;
        }
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
