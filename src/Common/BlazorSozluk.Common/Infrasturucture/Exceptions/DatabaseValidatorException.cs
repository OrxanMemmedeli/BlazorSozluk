using System.Runtime.Serialization;

namespace BlazorSozluk.Common.Infrasturucture.Exceptions;

public class DatabaseValidatorException : Exception
{
    public DatabaseValidatorException()
    {
    }

    public DatabaseValidatorException(string? message) : base(message)
    {
    }

    public DatabaseValidatorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DatabaseValidatorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
