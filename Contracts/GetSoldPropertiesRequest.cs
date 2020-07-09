using System.ComponentModel.DataAnnotations;

namespace RealEstate.Contracts
{
    public class GetSoldPropertiesRequest
    {
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Country { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string City { get; set; }
    }
}
