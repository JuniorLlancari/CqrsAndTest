namespace CQRS.Domain.Abstraccions
{
    public record Error(string code, string message)
    {
        public static Error None =new(string.Empty,string.Empty);
        public static Error NullValue = new Error("Error.NullValue", "Un valor nulo fue ingresado");
    }
}
