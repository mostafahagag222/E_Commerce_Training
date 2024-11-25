using Microsoft.AspNetCore.Builder;

namespace E_Commerce3APIs_V01.EnpointsHelper;

public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication app);
}
