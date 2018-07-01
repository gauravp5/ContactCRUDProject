using System.ComponentModel.DataAnnotations;
using ContactManager.Core.DomainModels;

namespace CRUD.ViewModels
{
    public class ContactViewModel
    {
        public ContactViewModel()
        {
            Status = Status.Active;
        }

        public ContactViewModel(Contact contact)
        {
            ID = contact.Id;
            FirstName = contact.FirstName;
            LastName = contact.LastName;
            Email = contact.Email;
            PhoneNumber = contact.PhoneNumber;
            Status = contact.Status;
        }

        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,10}|[0-9]{1,3})(\]?)$",ErrorMessage ="Not a valid Email.")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number.")]
        public string PhoneNumber { get; set; }
       
        public Status Status { get; set; }

        public Contact GetModel()
        {
            return new Contact
            {
                Id=ID,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Status = Status
            };
        }
    }
}