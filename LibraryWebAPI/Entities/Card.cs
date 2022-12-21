using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebAPI.Entities
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(19)]
        public string Number { get; set; } = null!;

        public decimal Balance { get; set; }

        [ForeignKey("PassportId")]
        public string AccountPassportId { get; set; } = null!;
        public Account? Account { get; set; }
    }
}
