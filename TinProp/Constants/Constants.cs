using TinProp.Constants.Interfaces;

namespace TinProp.Constants;

public class Constants : IConstants
{
    public string? ConnectionString { get; }
    
    public Constants(IConfiguration configuration)
    {
        ConnectionString = configuration["sql.connection"];
    }
}