using System.ComponentModel.DataAnnotations;

namespace CQRS.Domain
{
    public class DateInFutureAttribute :ValidationAttribute
    {
        private readonly Func<DateTime> _dateTimeNowProvider;

        public DateInFutureAttribute() :this(()=> DateTime.Now)
        {
            
        }

        public DateInFutureAttribute(Func<DateTime> dateTimeNowProvider)
        {
            _dateTimeNowProvider = dateTimeNowProvider;
            ErrorMessage = "La fecha debe ser del futuro";
        }

        public override bool IsValid(object value)
        {
            bool isValid = false;
            if (value is DateTime datetime)
            {
                isValid = datetime > _dateTimeNowProvider();
            }

            return isValid;
        }


    }
}
