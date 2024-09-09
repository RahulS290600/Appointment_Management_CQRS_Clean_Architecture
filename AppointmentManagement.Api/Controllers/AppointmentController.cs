using AppointmentManagement.Application.Features.Appointment.Commands.CreateAppointment;
using AppointmentManagement.Application.Features.Appointment.Commands.DeleteAppointment;
using AppointmentManagement.Application.Features.Appointment.Commands.UpdateAppointment;
using AppointmentManagement.Application.Features.Appointment.Queries.GetAllAppointments;
using AppointmentManagement.Application.Features.Appointment.Queries.GetAppointmentDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppointmentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        // GET: api/<AppointmentController>
        [HttpGet]
        public async Task<List<AppointmentDTO>> Get()
        {
            var appointments = await _mediator.Send(new GetAllAppointmentsQuery());
            return appointments;
        }

        // GET api/<AppointmentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDetailsDTO>> Get(int id)
        {
            var appointments = await _mediator.Send(new GetAppointmentDetailsQuery(id));
            return Ok(appointments);
        }

        // POST api/<AppointmentController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(CreateAppointmentCommand appointment)
        {
            var response = await _mediator.Send(appointment);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<AppointmentController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateAppointmentCommand appointment)
        {
            await _mediator.Send(appointment);
            return NoContent();
        }

        // DELETE api/<AppointmentController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteAppointmentCommand { AppointmentId = id};
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
