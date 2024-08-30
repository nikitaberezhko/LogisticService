using WebApi.Models.OtherModels;

namespace WebApi.Models.Response;

public class GetContainersListLocationResponse
{
    public List<ContainerApiModel> Containers { get; set; }
}