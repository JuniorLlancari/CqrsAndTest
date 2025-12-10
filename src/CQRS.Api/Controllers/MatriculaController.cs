using CQRS.Application.Handlers.Matriculas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaController : ControllerBase
    {

        private IMediator _mediator;

        public MatriculaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetMatriculaQueryRequest { }));
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Post(CreateMatriculaCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }


    }
}
