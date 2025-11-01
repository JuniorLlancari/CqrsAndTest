using Azure.Core;
using CQRS.Application.Alumnos;
using CQRS.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {

        private IMediator _mediator;

        public AlumnoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Post(CreateAlumnoRequest request)
        {
            return await _mediator.Send(request);           
        }

        [HttpGet]
        public async Task<ActionResult<List<AlumnoDto>>> Get()
        {
            return await _mediator.Send(new GetAlumnoQuery.GetAlumnoQueryRequest());
        }

    } 

}
