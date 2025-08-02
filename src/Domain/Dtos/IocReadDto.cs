namespace Domain.Dtos;

public class IocReadDto
{
    public int Id { get; set; }

    public string Sha256 { get; set; } = string.Empty;

    public string Sha1 { get; set; } = string.Empty;

    public string Md5 { get; set; } = string.Empty;

    public string Mcafee { get; set; } = string.Empty;

    public string Engines { get; set; } = string.Empty;
}
