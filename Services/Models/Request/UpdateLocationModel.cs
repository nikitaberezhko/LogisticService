namespace Services.Models.Request;

public class UpdateLocationModel
{
    public Guid Id { get; set; }
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
}