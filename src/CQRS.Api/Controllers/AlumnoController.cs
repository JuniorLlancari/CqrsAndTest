using CQRS.Application.ApplicationInsights;
using CQRS.Application.Handlers.Alumnos;
using CQRS.Common.Constants;
using CQRS.Domain.Models.ApplicationInsights;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {

        private IMediator _mediator;
        private readonly IInsertApplicationInsightsService _insertApplicationInsightsService;

        public AlumnoController(IMediator mediator, IInsertApplicationInsightsService insertApplicationInsightsService)
        {
            _mediator = mediator;
            _insertApplicationInsightsService = insertApplicationInsightsService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateAlumnoCommandRequest request)
        {

            var metric = new InsertApplicationInsightsModel(
            ApplicationInsightsConstants.METRIC_TYPE_API_CALL,
            EntitiesConstants.ALUMNO,"post-alumno");

            _insertApplicationInsightsService.Execute(metric);

            return Ok(await _mediator.Send(request));           
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var metric = new InsertApplicationInsightsModel(
            ApplicationInsightsConstants.METRIC_TYPE_API_CALL,
            EntitiesConstants.ALUMNO, "get-alumnos");

            _insertApplicationInsightsService.Execute(metric);

            var result = await _mediator.Send(new GetAlumnoQueryRequest());
            return Ok(result);
        }

    } 

}
