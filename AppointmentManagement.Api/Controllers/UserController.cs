using AppointmentManagement.Application.Features.Appointment.Commands.CreateAppointment;
using AppointmentManagement.Application.Features.Appointment.Commands.DeleteAppointment;
using AppointmentManagement.Application.Features.Appointment.Commands.UpdateAppointment;
using AppointmentManagement.Application.Features.Appointment.Queries.GetAllAppointments;
using AppointmentManagement.Application.Features.Appointment.Queries.GetAppointmentDetails;
using AppointmentManagement.Application.Features.User.Commands.CreateUser;
using AppointmentManagement.Application.Features.User.Commands.DeleteUser;
using AppointmentManagement.Application.Features.User.Commands.UpdateUser;
using AppointmentManagement.Application.Features.User.Queries.GetAllUsers;
using AppointmentManagement.Application.Features.User.Queries.GetUserDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppointmentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<List<UserDTO>> Get()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return users;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDTO>> Get(int id)
        {
            var users = await _mediator.Send(new GetUserDetailsQuery(id));
            return Ok(users);
        }

        // POST api/<UserController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(CreateUserCommand user)
        {
            var response = await _mediator.Send(user);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateUserCommand user)
        {
            await _mediator.Send(user);
            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteUserCommand { UserId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
