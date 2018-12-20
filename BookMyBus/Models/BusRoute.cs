using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookMyBus.Models
{
    public class BusRoute
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Source")]
        [Required]
        [StringLength(50, MinimumLength = 2,ErrorMessage="Source length must be between 2-50")]
        public string Source { get; set; }

      [Display(Name = "Destination")]
        [Required]
        [StringLength(50, MinimumLength = 2,ErrorMessage="Destination length must be between 2-50")]
       public string Destination { get; set; }

       [Display(Name = "Date of Journey")]
        [Required]
       [DataType(DataType.Date)]
      [CustomValidation(typeof(BusRoute), "ValidateDateOfJourney")]
        public DateTime DateOfJounery { get; set; }        

      [Display(Name = "Bus Number")]
        [Required]
         [RegularExpression(@"^[0-9]{1,3}$",ErrorMessage="Bus Number must be between 0-999")] 
        public int BusNumber { get; set; }

      [Display(Name = "Total Number of Tickets")]
       [Range(0, 100)] 
         public int? TotalNumberOfTickets { get; set; }

  [Display(Name = "Date Of Booking")]
        [Required]
       [DataType(DataType.Date)]
     [CustomValidation(typeof(BusRoute), "ValidateDateOfBooking")]
        public DateTime DateOfBooking { get;set; }

        public ICollection<Ticket> Tickets {get;set;}

 public string BusStatus {
        get {
            // IF INSPECTION NOT COMPLETED THEN
            // RETURN NG - "NO GRADE"
            if (DateOfJounery < DateTime.Now) {
                return "Departed";
            }

           
            // OTHERWISE RETURN F 
            return "Scheduled";
         }

         }

        
 public static ValidationResult ValidateDateOfJourney(DateTime DateOfJounery, ValidationContext context) {
            if (DateOfJounery == null) {
                return ValidationResult.Success;
            }

            if (DateOfJounery >= DateTime.Today ) {
                return ValidationResult.Success;
            }

            string errorMessage = $"Journey date must be a date on or after { DateTime.Today.ToShortDateString() }";
            return new ValidationResult(errorMessage);
        }
        public static ValidationResult ValidateDateOfBooking(DateTime DateOfBooking, ValidationContext context) {
            if (DateOfBooking == null) {
                return ValidationResult.Success;
            }
           var instance = context.ObjectInstance as BusRoute;
            if (DateOfBooking >= DateTime.Today && DateOfBooking <= instance.DateOfJounery) {
                return ValidationResult.Success;
            }

            string errorMessage = $"Booking date must be between today and journey date";
            return new ValidationResult(errorMessage);
        }
    }
}
