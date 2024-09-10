using AppointmentManagement.Application.Contracts.Logging;
using AppointmentManagement.Application.Contracts.Persistance;
using AppointmentManagement.Application.Exceptions;
using AppointmentManagement.Domain;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Application.Features.Appointment.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppLogger<UpdateAppointmentCommandHandler> _appLogger;

        public UpdateAppointmentCommandHandler(IMapper mapper, IAppointmentRepository appointmentRepository, IAppLogger<UpdateAppointmentCommandHandler> appLogger)
        {
            this._mapper = mapper;
            this._appointmentRepository = appointmentRepository;
            this._appLogger = appLogger;
        }
        public async Task<Unit> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            //Validate incoming data
            var validator = new UpdateAppointmentCommandValidator(_appointmentRepository);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _appLogger.LogWarning("Validation errors in update request");
                throw new BadRequestException("Invalid Request", validationResult);
            }
            //Convert to domiin entity type
            var appointmentToUpdate = _mapper.Map<Domain.Appointment>(request);
            if (appointmentToUpdate == null)
            {
                throw new NotFoundException(nameof(Appointment), request.VisitorName); // Handle the case where the appointment doesn't exist
            }
            _mapper.Map(request, appointmentToUpdate);
            //Add to database
            await _appointmentRepository.UpdateAsync(appointmentToUpdate);
            //Return unit value
            return Unit.Value;
        }
    }
}
