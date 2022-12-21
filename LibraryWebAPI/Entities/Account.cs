using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Entities
{
    public class Account
    {
        [MaxLength(99)]
        public string FirstName { get; set; } = null!;

        [MaxLength(99)]
        public string LastName { get; set; } = null!;

        [MaxLength(99)]
        public string Email { get; set; } = null!;

        [MaxLength(13)]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(9)]
        public string PassportId { get; set; } = null!;

        public ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}
