namespace WebApi.Models.OtherModels;

public class UpdateLocationApiModel
{
    public Guid Id { get; set; }
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
}