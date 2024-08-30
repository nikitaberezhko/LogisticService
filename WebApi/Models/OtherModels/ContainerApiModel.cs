namespace WebApi.Models.OtherModels;

public class ContainerApiModel
{
    public Guid Id { get; set; }
    
    public Guid OrderId { get; set; }
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    
    public DateTime LastUpdateTime { get; set; }
}