using WebApi.Models.OtherModels;

namespace WebApi.Models.Response;

public class GetContainersLocationResponse
{
    public List<ContainerApiModel> Containers { get; set; }
}