using Xunit;

namespace API.POC_MONGO.Tests.Fixtures
{
    [CollectionDefinition("Mapper")]
    public class MapperCollection : ICollectionFixture<MapperFixture>
    {
    }
}
