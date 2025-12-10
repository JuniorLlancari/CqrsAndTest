using CQRS.Domain.Models.ApplicationInsights;

namespace CQRS.Application.ApplicationInsights
{
    public interface IInsertApplicationInsightsService
    {
        bool Execute(InsertApplicationInsightsModel metric);
    }
}
