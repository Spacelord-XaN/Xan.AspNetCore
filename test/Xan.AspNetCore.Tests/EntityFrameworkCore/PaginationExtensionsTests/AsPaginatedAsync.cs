using Xan.AspNetCore.EntityFrameworkCore;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Tests.EntityFrameworkCore.PaginationExtensionsTests;

public class AsPaginatedAsync
    : TestBase
{
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task ShouldReturnCorrectPageSize()
    {
        //  Arrange
        Db.TestEntities.AddRange(_fixture.Build<TestEntity>().CreateMany(99));
        await Db.SaveChangesAsync();

        //  Act
        IPaginatedList<TestEntity> result = await Db.TestEntities.AsPaginatedAsync(10, 2);

        //  Assert
        using (new AssertionScope())
        {
            result.HasNextPage.Should().BeTrue();
            result.HasPreviousPage.Should().BeTrue();
            result.PageIndex.Should().Be(2);
            result.PageSize.Should().Be(10);
            result.TotalItemCount.Should().Be(99);
            result.TotalPageCount.Should().Be(10);
            result.Should().BeEquivalentTo(Db.TestEntities.Skip(10).Take(10));
        }
    }

    [Fact]
    public async Task WithSelector_ShouldReturnCorrectPageSize()
    {
        //  Arrange
        Db.TestEntities.AddRange(_fixture.Build<TestEntity>().CreateMany(99));
        await Db.SaveChangesAsync();

        //  Act
        IPaginatedList<string?> result = await Db.TestEntities.AsPaginatedAsync(10, 2, entity => entity.Name);

        //  Assert
        using (new AssertionScope())
        {
            result.HasNextPage.Should().BeTrue();
            result.HasPreviousPage.Should().BeTrue();
            result.PageIndex.Should().Be(2);
            result.PageSize.Should().Be(10);
            result.TotalItemCount.Should().Be(99);
            result.TotalPageCount.Should().Be(10);
            result.Should().BeEquivalentTo(Db.TestEntities.Skip(10).Take(10).Select(x => x.Name));
        }
    }

    [Fact]
    public async Task WithAsyncSelector_ShouldReturnCorrectPageSize()
    {
        //  Arrange
        Db.TestEntities.AddRange(_fixture.Build<TestEntity>().CreateMany(99));
        await Db.SaveChangesAsync();

        //  Act
        IPaginatedList<string?> result = await Db.TestEntities.AsPaginatedAsync(10, 2, async entity => await Task.FromResult(entity.Name));

        //  Assert
        using (new AssertionScope())
        {
            result.HasNextPage.Should().BeTrue();
            result.HasPreviousPage.Should().BeTrue();
            result.PageIndex.Should().Be(2);
            result.PageSize.Should().Be(10);
            result.TotalItemCount.Should().Be(99);
            result.TotalPageCount.Should().Be(10);
            result.Should().BeEquivalentTo(Db.TestEntities.Skip(10).Take(10).Select(x => x.Name));
        }
    }
}
