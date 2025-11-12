using CQRS.Application.Alumnos;
using CQRS.Application.DTOs;
using MediatR;
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
        public async Task<IActionResult> Post(CreateAlumnoCommandRequest request)
        {
            return Ok(await _mediator.Send(request));           
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAlumnoQueryRequest());
            return Ok(result);
        }

    } 

}
