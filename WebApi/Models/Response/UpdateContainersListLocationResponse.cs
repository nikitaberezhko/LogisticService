using WebApi.Models.OtherModels;

namespace WebApi.Models.Response;

public class UpdateContainersListLocationResponse
{
    public List<ContainerApiModel> Containers { get; set; }
}