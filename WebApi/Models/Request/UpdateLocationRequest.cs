namespace WebApi.Models.Request;

public class UpdateLocationRequest
{
    public Guid Id { get; set; }
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
}