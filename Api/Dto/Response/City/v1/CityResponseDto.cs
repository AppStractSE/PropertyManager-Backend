using Api.Dto.Response.Area.v1;

namespace Api.Dto.Response.City.v1;
public class CityResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IList<AreaResponseDto> Areas { get; set; }
}