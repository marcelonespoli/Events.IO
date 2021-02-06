using Events.IO.Domain.Core.Models;
using System;
using FluentValidation;

namespace Events.IO.Domain.Events
{
    public class Address : Entity<Address>
    {
        public Address(Guid id, string address1, string address2, string address3, string address4, string city, string county, string postCode, Guid? occasionId)
        {
            Id = id;
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            Address4 = address4;
            City = city;
            County = county;
            PostCode = postCode;
            OccasionId = occasionId;
        }

        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string Address3 { get; private set; }
        public string Address4 { get; private set; }
        public string City { get; private set; }
        public string County { get; private set; }
        public string PostCode { get; private set; }
        public Guid? OccasionId { get; private set; }

        // EF properties of navegation (let them vertual)
        public virtual Occasion Occasion { get; private set; }

        // Contructor for EF
        protected Address() { }

        public override bool IsValid()
        {
            RuleFor(r => r.Address1)
                .NotEmpty().WithMessage("")
                .Length(2, 150).WithMessage("");

            RuleFor(r => r.Address2)
                .NotEmpty().WithMessage("")
                .Length(2, 150).WithMessage("");

            RuleFor(r => r.City)
                .NotEmpty().WithMessage("")
                .Length(2, 150).WithMessage("");

            RuleFor(r => r.County)
                .NotEmpty().WithMessage("")
                .Length(2, 150).WithMessage("");

            RuleFor(r => r.PostCode)
                .NotEmpty().WithMessage("")
                .Length(2, 10).WithMessage("");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}