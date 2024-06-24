using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasWayLauncher.Model
{
    [Table("UserInformation")]
    public class UserInformation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        // Навигационное свойство
        public ICollection<UserMessages> UserMessages { get; set; }
    }
}
