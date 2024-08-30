namespace WebApi.Models.Request;

public class UpdateContainerLocationRequest
{
    public Guid Id { get; set; }
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
}