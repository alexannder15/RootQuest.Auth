namespace Domain.Dtos.Responses;

public class ResponseError
{
    public string Code { get; set; } = string.Empty;
    public string Mesage { get; set; } = string.Empty;
    public DateTimeOffset DateTime { get; set; } = DateTimeOffset.Now;
}
