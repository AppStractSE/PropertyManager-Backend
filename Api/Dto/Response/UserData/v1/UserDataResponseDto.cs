using Core.Domain;

namespace Api.Dto.Response.UserData.v1;
public class UserDataResponseDto
{
    public IList<UserTeamData> UserTeamsData { get; set; }
}