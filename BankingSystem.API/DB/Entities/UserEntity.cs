using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Entities
{
    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Id { get; set; }

        [Required, MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string FirstName { get; set; }

        [Required, MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string LastName { get; set; }

        [Required,MinLength(9), MaxLength(20)]
        [RegularExpression(@"^[0-9]+$")]
        public string IDNumber { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        [ForeignKey("Id")]
        public virtual IdentityUser IdentityUser { get; set; }
        public virtual ICollection<BankAccountEntity> BankAccountEntities { get; set; }
    }
}
