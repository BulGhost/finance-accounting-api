using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FinanceAccounting.DataAccess.Exceptions;
using FinanceAccounting.Domain;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace FinanceAccounting.WebApi.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseCrudController : ControllerBase
    {
        private readonly IMediator _mediator;

        protected IMediator Mediator =>
            _mediator ?? HttpContext.RequestServices.GetService<IMediator>();

        protected BaseCrudController(IMediator mediator)
        {
            _mediator = mediator;
        }

        internal int UserId =>
            User?.Identity == null || !User.Identity.IsAuthenticated
                ? 0
                : int.Parse(User.FindFirst(JwtRegisteredClaimNames.NameId).Value); //TODO: Modify
    }
}
