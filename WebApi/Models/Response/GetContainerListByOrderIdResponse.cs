using WebApi.Models.OtherModels;

namespace WebApi.Models.Response;

public class GetContainerListByOrderIdResponse
{
    public List<ContainerApiModel> Containers { get; set; }
}