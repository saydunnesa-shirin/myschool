using System.Runtime.Serialization;

namespace Api.Infrastructure.Exceptions;


[Serializable]
public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base($"{message}")
    {
    }

    public NotFoundException(string entity, string value)
        : base($"Entity {entity} with value {value} not found.")
    {
    }

    protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}