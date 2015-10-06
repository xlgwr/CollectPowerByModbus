using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EPMCS.Model
{
    public class KeyValParam : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string K { get; set; }

        [Required]
        [StringLength(400)]
        public string V { get; set; }
    }
}