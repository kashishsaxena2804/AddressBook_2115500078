using FluentValidation;
using ModelLayer.Models;

public class AddressBookEntryValidator : AbstractValidator<AddressBookEntry>
{
    public AddressBookEntryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid Email is required.");
        RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^\d{10}$").WithMessage("Phone number must be 10 digits.");
    }
}
