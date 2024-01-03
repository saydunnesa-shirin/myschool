using System.Text.Json;

namespace Api.Infrastructure;

public class UpperCaseEnumValueNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) => name.ToUpper();
}
