using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasWayLauncher.Model
{
    [Table("UserMessages")]
    public class UserMessages
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int username_id { get; set; } // Изменено на фактическое имя столбца

        [Required]
        [MaxLength(50)]
        public string message { get; set; } // Изменено на фактическое имя столбца

        // Навигационное свойство
        [ForeignKey("username_id")]
        public UserInformation UserInformation { get; set; }
    }
}
