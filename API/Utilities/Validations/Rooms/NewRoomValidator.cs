using API.DTOs.Rooms;
using FluentValidation;

namespace API.Utilities.Validations.Rooms;

public class NewRoomValidator : AbstractValidator<NewRoomDto>
{
    public NewRoomValidator()
    {
        RuleFor(room => room.Name)
            .NotEmpty();
        RuleFor(room => room.Capacity)
            .NotEmpty();
        RuleFor(room => room.Capacity)
            .NotEmpty();
    }
}