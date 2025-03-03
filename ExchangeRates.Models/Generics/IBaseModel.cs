
using System.ComponentModel.DataAnnotations;

namespace ExchangeRates.Models.Generics
{
    public interface IBaseModel
    {
        [Key]
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }

        void Created();
    }
}