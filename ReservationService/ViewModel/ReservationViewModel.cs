using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ReservationService.Models;

namespace ReservationService.ViewModel;

public partial class ReservationViewModel
{


    [Range(0, int.MaxValue, ErrorMessage = "Number of children must be a non-negative value.")]
    public int NumberOfChildren { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Number of adults must be at least 1.")]
    [Required]
    public int NumberOfAdults { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Check-in date is required.")]
    public DateTime CheckInDate { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Check-out date is required.")]
    public DateTime CheckOutDate { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Number of nights must be at least 1.")]
    public int NumberOfNights { get; set; }

    [Required]
    public int RoomId { get; set; }


    [Required(ErrorMessage = "Please provide a phone number.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    [RegularExpression(@"^\+91\s[7-9]\d{9}$", ErrorMessage = "Phone number must be in the format (+91) 98765 43210.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Please provide your company name.")]
    [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
    public string Company { get; set; }

    [Required(ErrorMessage = "Please provide your full name.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please provide your email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please specify your gender.")]
    [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters.")]
    public string Gender { get; set; }

    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
    public string Address { get; set; }

    [Required]
    public decimal Price { get; set; } 


    public int CalculateNumOfNights()
    {
        NumberOfNights = Math.Max((CheckOutDate - CheckInDate).Days, 1);
        return NumberOfNights;
    }

    public decimal CalculatePrice(decimal Rate, int NumberOfDays)//decimal Discount
    {
        // Price = (Rate*NumberOfDays) - Discount;
        Price = (Rate * NumberOfDays);

        return Price;
    }
}
