using WebApi.Models.OtherModels;

namespace WebApi.Models.Request;

public class UpdateContainersLocationRequest
{
    public List<UpdateLocationApiModel> ContainersList { get; set; }
}