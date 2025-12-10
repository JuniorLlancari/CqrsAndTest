namespace CQRS.Domain.Models.ApplicationInsights
{
    public class InsertApplicationInsightsModel
    {
  

        public InsertApplicationInsightsModel(string type, string content, string detail)
        {
            Type = type;
            Id = Guid.NewGuid().ToString();
            Content = content;
            Detail = detail;
        }

        public string Type { get; set; }
        public string Id { get; set; }
        public string Content { get; set; }
        public string Detail { get; set; }

    }
}
