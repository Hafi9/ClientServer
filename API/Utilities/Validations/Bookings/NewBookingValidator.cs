using API.DTOs.Bookings;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.Utilities.Validations.Bookings;

public class NewBookingValidator : AbstractValidator<NewBookingDto>
{
    public NewBookingValidator()
    {
        RuleFor(b => b.StartDate)
            .NotEmpty();
        RuleFor(b => b.EndDate)
            .NotEmpty();
        RuleFor(b => b.Status)
            .NotNull()
            .IsInEnum();
        RuleFor(b => b.Remarks)
            .NotEmpty();
        RuleFor(b => b.EmployeeGuid)
            .NotEmpty();
        RuleFor(b => b.RoomGuid)
            .NotEmpty();
    }
}