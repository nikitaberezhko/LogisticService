using Services.Models.OtherModels;

namespace Services.Models.Request;

public class UpdateContainersLocationModel
{
    public List<ContainerUpdateModel> ContainersList { get; set; }
}