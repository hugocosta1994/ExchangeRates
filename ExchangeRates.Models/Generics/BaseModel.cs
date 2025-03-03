using System.ComponentModel.DataAnnotations;

namespace ExchangeRates.Models.Generics
{
    public abstract class BaseModel : IBaseModel
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public void Created()
        {
            this.CreatedAt = DateTime.UtcNow;
        }
    }
}
