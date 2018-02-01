using Xunit;

namespace NameSearch.App.Tests
{
    /// <summary>
    /// Unit tests for Dependency Injection Container assignment to Static Properties Container
    /// </summary>
    /// <seealso cref="NameSearch.App.Program" />
    public class StaticServiceCollectionTests : Program
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticServiceCollectionTests"/> class.
        /// </summary>
        public StaticServiceCollectionTests() : base()
        {
            Program.Main(null);
        }

        /// <summary>
        /// Mappers the is valid.
        /// </summary>
        [Fact]
        public void Mapper_IsValid()
        {
            Assert.NotNull(StaticServiceCollection.Mapper);
        }

        /// <summary>
        /// Repositories the is valid.
        /// </summary>
        [Fact]
        public void Repository_IsValid()
        {
            Assert.NotNull(StaticServiceCollection.Repository);
        }

        /// <summary>
        /// Serializers the settings is valid.
        /// </summary>
        [Fact]
        public void SerializerSettings_IsValid()
        {
            Assert.NotNull(StaticServiceCollection.SerializerSettings);
        }
    }
}