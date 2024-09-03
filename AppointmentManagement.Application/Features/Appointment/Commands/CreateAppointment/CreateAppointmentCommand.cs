﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Application.Features.Appointment.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : IRequest<int>
    {
        public string VisitorName { get; set; } = string.Empty;
        public string Consultant { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;

    }
}