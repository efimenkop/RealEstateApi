using System.ComponentModel.DataAnnotations;

namespace RealEstateApi
{
    public class GetSoldPropertiesRequest
    {
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Country { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string City { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Address { get; set; }

        [MinLength(1)]
        [MaxLength(100)]
        public int? PageSize { get; set; }
    }
}
