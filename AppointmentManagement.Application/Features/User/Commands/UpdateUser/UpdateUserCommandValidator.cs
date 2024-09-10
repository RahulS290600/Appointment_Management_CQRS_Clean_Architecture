﻿using AppointmentManagement.Application.Contracts.Persistance;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Application.Features.User.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandValidator(IUserRepository userRepository)
        {
            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number cannot be empty")
                .NotNull()
                .MaximumLength(10).WithMessage("Maximum length should have 10 characters")
                .MinimumLength(10).WithMessage("Phone number should have 10 charaters");
            this._userRepository = userRepository;
        }
    }
}
