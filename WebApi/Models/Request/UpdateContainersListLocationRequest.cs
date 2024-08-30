using WebApi.Models.OtherModels;

namespace WebApi.Models.Request;

public class UpdateContainersListLocationRequest
{
    public List<UpdateContainerLocationApiModel> ContainersList { get; set; }
}