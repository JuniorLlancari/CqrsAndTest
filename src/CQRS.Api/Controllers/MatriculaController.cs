using CQRS.Application.Cursos;
using CQRS.Application.DTOs;
using CQRS.Application.Matriculas;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CQRS.Application.Matriculas.CreateMatriculaCommand;

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
        public async Task<ActionResult<List<MatriculaDto>>> Get()
        {
            return await _mediator.Send(new GetMatriculaQuery.GetMatriculaQueryRequest { });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Post(CreateMatriculaCommandRequest request)
        {
            return await _mediator.Send(request);
        }


    }
}
