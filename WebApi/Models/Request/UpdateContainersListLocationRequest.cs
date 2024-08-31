using WebApi.Models.OtherModels;

namespace WebApi.Models.Request;

public class UpdateContainersListLocationRequest
{
    public List<UpdateLocationApiModel> ContainersList { get; set; }
}