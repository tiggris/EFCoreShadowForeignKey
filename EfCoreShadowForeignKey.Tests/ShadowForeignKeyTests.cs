using EFCoreShadowForeignKey.Data;
using EFCoreShadowForeignKey.Model;
using EFCoreShadowForeignKey.Tests.Utils;
using Microsoft.EntityFrameworkCore;

namespace EFCoreShadowForeignKey.Tests
{
    [Collection("Shadow foreign key test collection")]
    public class ShadowForeignKeyTests
    {
        [Fact]
        public void QueryEntities_ShouldNotThrowException()
        {
            // Arrange
            
            // Act
            using (var dataContext = new DataContext(DatabaseFixture.DbContextOptions))
            {
                var entities = dataContext.Set<LeafEntity>().ToList();
            }

            // Assert            
        }
    }
}