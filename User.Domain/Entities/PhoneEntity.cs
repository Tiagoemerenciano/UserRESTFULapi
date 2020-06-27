using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace User.Domain
{
    public class PhoneEntity
    {
        [Key]
        public int PhoneId { get; set; }
        public int Number { get; set; }
        [JsonPropertyName("area_code")]
        public int AreaCode { get; set; }
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
    }
}
