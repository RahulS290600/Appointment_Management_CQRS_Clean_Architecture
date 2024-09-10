using AppointmentManagement.Application.Contracts.Persistance;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Application.Features.Appointment.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public UpdateAppointmentCommandValidator(IAppointmentRepository appointmentRepository)
        {
            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number cannot be empty")
                .NotNull()
                .MaximumLength(10).WithMessage("Maximum length should have 10 characters")
                .MinimumLength(10).WithMessage("Phone number should have 10 charaters");

            RuleFor(p => p.Date)
                .GreaterThan(DateTime.UtcNow.Date)
                .WithMessage("Can't take appointment for this date");
            this._appointmentRepository = appointmentRepository;
        }
    }
}
