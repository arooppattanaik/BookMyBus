using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookMyBus.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Seat Number")]
         [Range(1, 100)] 
         [Required]
        public int SeatNumber { get; set; }

        [Display(Name = "Ticket Price")]
         [Required]
        [Range(0, 1000000000)] 
        public decimal Price { get; set; }

       [Display(Name = "Date Of Booking")]
        [Required]
       [DataType(DataType.Date)]
      [CustomValidation(typeof(Ticket), "ValidateDateOfBooking")]
        public DateTime DateOfBooking { get; set; }

        [Display(Name = "Date of Journey")]
        [Required]
       [DataType(DataType.Date)]
      [CustomValidation(typeof(Ticket), "ValidateDateOfJourney")]
        public DateTime DateOfJounery { get; set; }
        
         public Customer Customer{ get; set; }
         [Display(Name = "Bus Route")]
        [CustomValidation(typeof(Ticket), "BusSeatValidation")]
         public int BusRouteId { get; set; }
        public BusRoute BusRoute { get; set; }
           

       public static ValidationResult BusSeatValidation(int BusRouteId , ValidationContext context) {
        if (BusRouteId == 0) {
            return ValidationResult.Success;
        }
        var instance = context.ObjectInstance as Ticket;
        var bus = context.ObjectInstance as BusRoute;
        if (instance == null) {
            return ValidationResult.Success;
        }

        var dbContext = context.GetService(typeof(AppDbContext)) as AppDbContext;
        
       

        bus =dbContext.BusRoutes.Find(BusRouteId);
        
        int? Seater = bus.TotalNumberOfTickets;
    
        int duplicateCount = dbContext.Ticket
                                      .Count(x =>x.Id != instance.Id && x.BusRouteId == BusRouteId);
        if (duplicateCount >= Seater) {
            return new ValidationResult(" Sorry!! Ticket for this bus is not available");
        }
        return ValidationResult.Success; 
    }  

    public string TicketClass {
        get {
            // IF INSPECTION NOT COMPLETED THEN
            // RETURN NG - "NO GRADE"
            if (Price < 10) {
                return "Economy";
            }

            if (Price > 10 && Price < 50) {
                return "Business";
            }
            // OTHERWISE RETURN F 
            return "First Class";
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
           var instance = context.ObjectInstance as Ticket;
            if (DateOfBooking >= DateTime.Today && DateOfBooking <= instance.DateOfJounery) {
                return ValidationResult.Success;
            }

            string errorMessage = $"Booking date must be between today and journey date";
            return new ValidationResult(errorMessage);
        }
        


    }
}
