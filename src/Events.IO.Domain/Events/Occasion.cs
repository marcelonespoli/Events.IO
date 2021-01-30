using System;
using System.Collections.Generic;
using System.Text;
using Events.IO.Domain.Core.Models;
using System.Linq;
using FluentValidation;
using Events.IO.Domain.Organizers;

namespace Events.IO.Domain.Events
{
    public class Occasion : Entity<Occasion>
    {
        public Occasion(
            string name,
            DateTime startDate,
            DateTime endDate,
            bool free,
            decimal value,
            bool online,
            string companyName)
        {
            Id = Guid.NewGuid();
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Free = free;
            Value = value;
            Online = online;
            CompanyName = companyName;
        }

        public string Name { get; private set; }
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool Free { get; private set; }
        public decimal Value { get; private set; }
        public bool Online { get; private set; }
        public string CompanyName { get; private set; }
        public bool Excluded { get; private set; }
        public ICollection<Tags> Tags { get; private set; }


        public Category Category { get; private set; }
        public Address Address { get; private set; }
        public Organizer Organizer { get; private set; }


        public override bool IsValid()
        {
            Validate();
            return ValidationResult.IsValid;
        }

        #region Validation
        private void Validate()
        {
            ValidateName();
            ValidateValue();
            ValidateStartDate();
            ValidateEndDate();
            ValidateAddress();
            ValidateCompanyName(); 
            ValidationResult = Validate(this);
        }

        private void ValidateName()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("The name needs to be informed")
                .Length(2, 150).WithMessage("The name must be between 2 to 150 characters");
        }

        private void ValidateValue()
        {
            if (!Free)
                RuleFor(p => p.Value)
                    .GreaterThan(1).WithMessage("For not free event the value must be greater than 0");

            if (Free)
                RuleFor(p => p.Value)
                    .ExclusiveBetween(0, 0).When(p => p.Free)
                    .WithMessage("For free event the value must not be greater than 0");
        }

        private void ValidateStartDate()
        {
            RuleFor(p => p.StartDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("The start date cannot be lower than today");

            RuleFor(p => p.StartDate)
                .LessThanOrEqualTo(p => p.EndDate)
                .WithMessage("The start date cannot be greater than the end date");

            if (EndDate != null)
                RuleFor(p => p.StartDate)
                    .NotNull().When(p => EndDate != null)
                    .WithMessage("If there is a end date please inform the start date");
        }

        private void ValidateEndDate()
        {
            RuleFor(p => p.EndDate)
                .GreaterThanOrEqualTo(p => p.StartDate)
                .WithMessage("End date cannot be less than start date");

            if (StartDate != null)
                RuleFor(p => p.EndDate)
                    .NotNull().When(p => p.StartDate != null)
                    .WithMessage("If there is a start date please inform the end date");
        }

        private void ValidateAddress()
        {
            if (Online)
                RuleFor(p => p.Address)
                    .Null().When(p => p.Online)
                    .WithMessage("The event must not has an addrees for online event");

            if (!Online)
                RuleFor(p => p.Address)
                    .NotNull().When(p => p.Online == false)
                    .WithMessage("The event must has an address for a not online event");
        }

        private void ValidateCompanyName()
        {
            RuleFor(p => p.CompanyName)
                .NotEmpty().WithMessage("The organizer name need to be informed")
                .Length(2, 150).WithMessage("The company name must be between 2 to 150 characters");
        }
        #endregion 
    }
}
