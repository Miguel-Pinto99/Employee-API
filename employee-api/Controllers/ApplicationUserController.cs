using Microsoft.AspNetCore.Mvc;
using employee_api.Persistance;
using employee_api.Infrastructure;
using MediatR;
using employee_api.Application.ApplicationUsers.Queries.GetApplicationUser;
using employee_api.Application.ApplicationUsers.Queries.EditApplicationUser;
using employee_api.Application.ApplicationUsers.Queries.GetAllApplicationUser;
using employee_api.Application.ApplicationUsers.Queries.CheckInApplicationUser;
using employee_api.Application.ApplicationUsers.Queries.CheckOutApplicationUser;
using employee_api.Application.ApplicationUsers.Commands.CreateApplicationUser;
using employee_api.Application.ApplicationUsers.Commands.DeleteApplicationUser;

namespace employee_api.Controllers
{
    [ApiController]
    [Route(template: "api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(int id, [FromBody] CreateApplicationUserCommandBody body, CancellationToken cancellationToken)
        {
            var command = new CreateApplicationUserCommand(id, body);
            var response = await _mediator.Send(command, cancellationToken);
            if (response != null)
            {
                return Ok(response.ApplicationUser);
            }
            else
            {
                return BadRequest(response.ApplicationUser);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id, [FromBody]EditApplicationUserCommandBody body, CancellationToken cancellationToken)
        {
            var command = new EditApplicationUserCommand(id, body);
            var response = await _mediator.Send(command, cancellationToken);
            if (response != null)
            {
                return Ok(response.ApplicationUser);
            }
            else
            {
                return BadRequest(response.ApplicationUser);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllApplicationUserAsync(CancellationToken cancellationToken)
        {
            var command = new GetAllApplicationUserCommand();
            var response = await _mediator.Send(command, cancellationToken);
            if (response != null)
            {
                return Ok(response.listApplicationUser);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id,CancellationToken cancellationToken)
        {
            var command = new GetApplicationUserCommand(id);
            var response = await _mediator.Send(command, cancellationToken);

            if (response != null)
            {
                return Ok(response.ApplicationUser);
            }
            else
            {
                return BadRequest(response.ApplicationUser);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteApplicationUserCommand(id);
            var response = await _mediator.Send(command, cancellationToken);

            if (response != null)
            {
                return Ok(response.ApplicationUser);
            }
            else
            {
                return BadRequest(response.ApplicationUser);
            }
        }
        [HttpPost("{id}/check-in")]
        public async Task<IActionResult> CheckInAsync(int id, CancellationToken cancellationToken)
        {
            var command = new CheckInApplicationUserCommand(id);
            var response = await _mediator.Send(command, cancellationToken);

            if (response != null)
            {
                return Ok(response.ApplicationUser);
            }
            else
            {
                return BadRequest(response.ApplicationUser);
            }

        }
        [HttpPost("{id}/check-out")]
        public async Task<IActionResult> CheckOutAsync(int id, CancellationToken cancellationToken)
        {
            var command = new CheckOutApplicationUserCommand(id);
            var response = await _mediator.Send(command, cancellationToken);

            if (response != null)
            {
                return Ok(response.ApplicationUser);
            }
            else
            {
                return BadRequest(response.ApplicationUser);
            }
        }
    }
}