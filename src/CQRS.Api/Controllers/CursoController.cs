using CQRS.Application.Cursos;
using CQRS.Application.DTOs;
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
        public async Task<ActionResult<List<CursoDto>>> Get()
        {
            return await _mediator.Send(new GetCursoQuery.GetCursoQueryRequest());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Post(CreateCursoCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDto>> Get(Guid id)
        {
            return await _mediator.Send(new GetCursoQueryById.GetCursoQueryByIdRequest { Id = id });
        }


    }
}
