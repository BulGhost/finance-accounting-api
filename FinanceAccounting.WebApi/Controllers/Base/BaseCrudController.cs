using System;
using System.Collections.Generic;
using FinanceAccounting.DataAccess.Exceptions;
using FinanceAccounting.Domain;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace FinanceAccounting.WebApi.Controllers.Base
{
    [ApiController]
    public abstract class BaseCrudController<T, TController> : ControllerBase
        where T : BaseEntity, new()
        where TController : BaseCrudController<T, TController>
    {
        protected readonly IRepository<T> MainRepo;
        protected readonly ILogger<TController> Logger;

        protected BaseCrudController(IRepository<T> repo, ILogger<TController> logger)
        {
            MainRepo = repo;
            Logger = logger;
        }
    }
}
