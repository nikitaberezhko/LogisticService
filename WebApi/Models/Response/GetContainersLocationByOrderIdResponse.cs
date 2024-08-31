using WebApi.Models.OtherModels;

namespace WebApi.Models.Response;

public class GetContainersLocationByOrderIdResponse
{
    public List<ContainerApiModel> Containers { get; set; }
}