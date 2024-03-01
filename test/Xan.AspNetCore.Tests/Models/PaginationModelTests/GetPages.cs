using Xan.AspNetCore.Models;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Tests.Models.PaginationModelTests;

public class GetPages
{
    public static object[][] ShouldThrowArgumentOutOfRangeExceptionTestData =
    [
        [ int.MaxValue, int.MinValue ],
        [ int.MaxValue, 0 ],
        [ 0, 1 ],
        [ int.MinValue, 1 ],
        [ int.MinValue, int.MaxValue ]
    ];

    [Theory]
    [MemberData(nameof(ShouldThrowArgumentOutOfRangeExceptionTestData))]
    public void Static_ShouldThrowOutOfRangeException(int totalPages, int pageIndex)
    {
        //  Arrange

        //  Act
        Action act = () => PaginationModel.GetPages(pageIndex, totalPages).ToArray();

        //  Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [MemberData(nameof(ShouldThrowArgumentOutOfRangeExceptionTestData))]
    public void Instance_ShouldThrowOutOfRangeException(int totalPages, int pageIndex)
    {
        //  Arrange
        PaginationModel paginationModel = new(new PaginatedList<object>([], pageIndex, 0, totalPages, 0), new ListParameter(), parameter => string.Empty);

        //  Act
        Action act = () => paginationModel.GetPages().ToArray();

        //  Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    public static object[][] ShouldReturnExpectedPagesTestData =
    [
        [1, 1, new int? [] { 1 }],

        [2, 1, new int? [] { 1, 2 }],
        [2, 2, new int? [] { 1, 2 }],

        [3, 1, new int? [] { 1, 2, 3 }],
        [3, 2, new int? [] { 1, 2, 3 }],
        [3, 3, new int? [] { 1, 2, 3 }],

        [4, 1, new int? [] { 1, 2, 3, 4 }],
        [4, 2, new int? [] { 1, 2, 3, 4 }],
        [4, 3, new int? [] { 1, 2, 3, 4 }],
        [4, 4, new int? [] { 1, 2, 3, 4 }],

        [5, 1, new int? [] { 1, 2, 3, 4, 5 }],
        [5, 2, new int? [] { 1, 2, 3, 4, 5 }],
        [5, 3, new int? [] { 1, 2, 3, 4, 5 }],
        [5, 4, new int? [] { 1, 2, 3, 4, 5 }],
        [5, 5, new int? [] { 1, 2, 3, 4, 5 }],

        [6, 1, new int? [] { 1, 2, 3, null, 6 }],
        [6, 2, new int? [] { 1, 2, 3, 4, 5, 6 }],
        [6, 3, new int? [] { 1, 2, 3, 4, 5, 6 }],
        [6, 4, new int? [] { 1, 2, 3, 4, 5, 6 }],
        [6, 5, new int? [] { 1, 2, 3, 4, 5, 6 }],
        [6, 6, new int? [] { 1, null, 4, 5, 6 }],

        [7, 1, new int? [] { 1, 2, 3, null, 7 }],
        [7, 2, new int? [] { 1, 2, 3, 4, null, 7 }],
        [7, 3, new int? [] { 1, 2, 3, 4, 5, 6, 7 }],
        [7, 4, new int? [] { 1, 2, 3, 4, 5, 6, 7 }],
        [7, 5, new int? [] { 1, 2, 3, 4, 5, 6, 7 }],
        [7, 6, new int? [] { 1, null, 4, 5, 6, 7 }],
        [7, 7, new int? [] { 1, null, 5, 6, 7 }],

        [8, 1, new int? [] { 1, 2, 3, null, 8 }],
        [8, 2, new int? [] { 1, 2, 3, 4, null, 8 }],
        [8, 3, new int? [] { 1, 2, 3, 4, 5, null, 8 }],
        [8, 4, new int? [] { 1, 2, 3, 4, 5, 6, 7, 8 }],
        [8, 5, new int? [] { 1, 2, 3, 4, 5, 6, 7, 8 }],
        [8, 6, new int? [] { 1, null, 4, 5, 6, 7, 8 }],
        [8, 7, new int? [] { 1, null, 5, 6, 7, 8 }],
        [8, 8, new int? [] { 1, null, 6, 7, 8 }],

        [9, 1, new int? [] { 1, 2, 3, null, 9 }],
        [9, 2, new int? [] { 1, 2, 3, 4, null, 9 }],
        [9, 3, new int? [] { 1, 2, 3, 4, 5, null, 9 }],
        [9, 4, new int? [] { 1, 2, 3, 4, 5, 6, null, 9 }],
        [9, 5, new int? [] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }],
        [9, 6, new int? [] { 1, null, 4, 5, 6, 7, 8, 9 }],
        [9, 7, new int? [] { 1, null, 5, 6, 7, 8, 9 }],
        [9, 8, new int? [] { 1, null, 6, 7, 8, 9 }],
        [9, 9, new int? [] { 1, null, 7, 8, 9 }],

        [10, 1, new int? [] { 1, 2, 3, null, 10 }],
        [10, 2, new int? [] { 1, 2, 3, 4, null, 10 }],
        [10, 3, new int? [] { 1, 2, 3, 4, 5, null, 10 }],
        [10, 4, new int? [] { 1, 2, 3, 4, 5, 6, null, 10 }],
        [10, 5, new int? [] { 1, 2, 3, 4, 5, 6, 7, null, 10 }],
        [10, 6, new int? [] { 1, null, 4, 5, 6, 7, 8, 9, 10 }],
        [10, 7, new int? [] { 1, null, 5, 6, 7, 8, 9, 10 }],
        [10, 8, new int? [] { 1, null, 6, 7, 8, 9, 10 }],
        [10, 9, new int? [] { 1, null, 7, 8, 9, 10 }],
        [10, 10, new int? [] { 1, null, 8, 9, 10 }],

        [11, 1, new int? [] { 1, 2, 3, null, 11 }],
        [11, 2, new int? [] { 1, 2, 3, 4, null, 11 }],
        [11, 3, new int? [] { 1, 2, 3, 4, 5, null, 11 }],
        [11, 4, new int? [] { 1, 2, 3, 4, 5, 6, null, 11 }],
        [11, 5, new int? [] { 1, 2, 3, 4, 5, 6, 7, null, 11 }],
        [11, 6, new int? [] { 1, null, 4, 5, 6, 7, 8, null, 11 }],
        [11, 7, new int? [] { 1, null, 5, 6, 7, 8, 9, 10, 11 }],
        [11, 8, new int? [] { 1, null, 6, 7, 8, 9, 10, 11 }],
        [11, 9, new int? [] { 1, null, 7, 8, 9, 10, 11 }],
        [11, 10, new int? [] { 1, null, 8, 9, 10, 11 }],
        [11, 11, new int? [] { 1, null, 9, 10, 11 }],

        [12, 1, new int? [] { 1, 2, 3, null, 12 }],
        [12, 2, new int? [] { 1, 2, 3, 4, null, 12 }],
        [12, 3, new int? [] { 1, 2, 3, 4, 5, null, 12 }],
        [12, 4, new int? [] { 1, 2, 3, 4, 5, 6, null, 12 }],
        [12, 5, new int? [] { 1, 2, 3, 4, 5, 6, 7, null, 12 }],
        [12, 6, new int? [] { 1, null, 4, 5, 6, 7, 8, null, 12 }],
        [12, 7, new int? [] { 1, null, 5, 6, 7, 8, 9, null, 12 }],
        [12, 8, new int? [] { 1, null, 6, 7, 8, 9, 10, 11, 12 }],
        [12, 9, new int? [] { 1, null, 7, 8, 9, 10, 11, 12 }],
        [12, 10, new int? [] { 1, null, 8, 9, 10, 11, 12 }],
        [12, 11, new int? [] { 1, null, 9, 10, 11, 12 }],
        [12, 12, new int? [] { 1, null, 10, 11, 12 }],

        [100, 1, new int? [] { 1, 2, 3, null, 100 }],
        [100, 50, new int? [] { 1, null, 48, 49, 50, 51, 52, null, 100 }],
        [100, 100, new int? [] { 1, null, 98, 99, 100 }]
    ];

    [Theory]
    [MemberData(nameof(ShouldReturnExpectedPagesTestData))]
    public void Static_ShouldReturnExpectedPages(int totalPages, int pageIndex, int?[] expectedPages)
    {
        //  Arrange

        //  Act
        IEnumerable<int?> result = PaginationModel.GetPages(pageIndex, totalPages);

        //  Assert
        result.Should().BeEquivalentTo(expectedPages);
    }

    [Theory]
    [MemberData(nameof(ShouldReturnExpectedPagesTestData))]
    public void Instance_ShouldReturnExpectedPages(int totalPages, int pageIndex, int?[] expectedPages)
    {
        //  Arrange
        PaginationModel paginationModel = new(new PaginatedList<object>([], pageIndex, 0, totalPages, 0), new ListParameter(), parameter => string.Empty);

        //  Act
        IEnumerable<int?> result = paginationModel.GetPages();

        //  Assert
        result.Should().BeEquivalentTo(expectedPages);
    }
}
