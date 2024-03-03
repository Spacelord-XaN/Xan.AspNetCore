﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.TableBuilderExtensionsTests;

public class UpdatedAtColumn
    : TestBase
{
    [Theory]
    [AutoData]
    public void ShouldReturnCorrectHtml(TestEntity item)
    {
        // Arrange
        TableBuilder<CrudItemModel<TestEntity>> sut = CreateSut([new CrudItemModel<TestEntity>(item, false)]);

        // Act
        TagBuilder result = sut.UpdatedAtColumn().Build();

        // Assert
        result.Should().BeHtml($"""<table><thead><tr><th scope="col" style="width: auto;text-align: left;white-space: nowrap;">Xan_AspNetCore_UpdatedAt</th></tr></thead><tbody><tr><td style="width: auto;text-align: left;white-space: nowrap;">{item.UpdatedAt.ToHtmlTimeStampDisplay()}</td></tr></tbody></table>""");
    }
}
