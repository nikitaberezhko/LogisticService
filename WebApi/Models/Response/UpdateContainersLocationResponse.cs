using WebApi.Models.OtherModels;

namespace WebApi.Models.Response;

public class UpdateContainersLocationResponse
{
    public List<ContainerApiModel> Containers { get; set; }
}