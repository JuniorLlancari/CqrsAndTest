using CQRS.Application.Handlers.Cursos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private IMediator _mediator;

        public CursoController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetCursoQueryRequest()));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCursoCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _mediator.Send(new GetCursoQueryByIdRequest { Id = id }));
        }


    }
}
