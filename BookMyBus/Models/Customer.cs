using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookMyBus.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2,ErrorMessage="Name length must be between 2-50")]
        public string Name { get; set; }

        [Display(Name = "Phone Number")]
        [Required] 
        public int PhoneNumber { get; set; }
     [Display(Name = "Date Of Birth")]
        [Required]
       [DataType(DataType.Date)]
      [CustomValidation(typeof(Customer), "BirthDateValidation")]
        public DateTime DateOfBirth { get; set; }

        
        public char Gender { get; set; }

      [Display(Name = "Ticket")]
      [CustomValidation(typeof(Customer), "TicketIDValidation")]
        public int TicketId { get; set; }
           public Ticket Ticket{ get; set; }

           public int Age {
        get {        
int age = DateTime.Today.Year - DateOfBirth.Year;
            return age;
         }
    }

        
public static ValidationResult BirthDateValidation(DateTime DateOfBirth, ValidationContext context) {
            if (DateOfBirth == null) {
                return ValidationResult.Success;
            }

            if (DateOfBirth <= DateTime.Today) {
                return ValidationResult.Success;
            }

            string errorMessage = $"Birth date must be a date on or before { DateTime.Today.ToShortDateString() }";
            return new ValidationResult(errorMessage);
        }

 public static ValidationResult TicketIDValidation(int TicketId , ValidationContext context) {
        if (TicketId == 0) {
            return ValidationResult.Success;
        }
        var instance = context.ObjectInstance as Customer;
        if (instance == null) {
            return ValidationResult.Success;
        }

        var dbContext = context.GetService(typeof(AppDbContext)) as AppDbContext;

        int duplicateCount = dbContext.Customer
                                      .Count(x =>x.Id != instance.Id && x.TicketId ==TicketId);
        if (duplicateCount > 0) {
            return new ValidationResult("This Ticket Id is already linked with a customer please check th details");
        }
        return ValidationResult.Success; 
    }
    }
}
