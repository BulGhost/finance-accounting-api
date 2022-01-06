using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using FinanceAccounting.Application.Users.Commands.RefreshToken;
using FinanceAccounting.Application.Users.Commands.RegisterUser;
using FinanceAccounting.Application.Users.Queries.AuthenticateUser;
using FinanceAccounting.Application.Users.Queries.LogoutUser;
using FinanceAccounting.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace FinanceAccounting.WebApi.Controllers
{
    [Route("api/[action]")]
    public class UserController : BaseCrudController
    {
        private readonly TokenValidationParameters _tokenValidationParameters;

        public UserController(IMediator mediator, TokenValidationParameters tokenValidationParameters)
            : base(mediator)
        {
            _tokenValidationParameters = tokenValidationParameters;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            UserRegistrationResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserQuery query)
        {
            UserAuthenticationResponse response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            UserAuthenticationResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        public async Task<IActionResult> Logout()
        {
            var query = new LogoutUserQuery(UserId);
            await Mediator.Send(query);
            return NoContent();
        }
    }
}
