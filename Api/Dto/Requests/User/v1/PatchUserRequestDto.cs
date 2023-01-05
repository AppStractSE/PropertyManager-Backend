namespace Api.Dto.Request.User.v1;

public class PatchUserRequestDto
{
    public Guid Id { get; set; }
    public string CredId { get; set; }
    public string RoleId { get; set; }
    public string Name { get; set; }
}