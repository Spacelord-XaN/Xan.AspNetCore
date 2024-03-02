﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.DefaultBoostrapHtmlFactoryTests;

public class DataListField
{
    [Fact]
    public void WithAll()
    {
        //  Arrange
        ViewContext viewContext = new();
        DefaultBoostrapHtmlFactory factory = new(new StringLocalizerMock());
        factory.Contextualize(viewContext);
        HashSet<string?> items = new() { "Item1", "Item2" };

        //  Act
        TagBuilder result = factory.DataListField("MyName", "MyValue", items, "MyTitle", autoFocus: true);

        //  Assert
        result.Should().BeHtml("""<div class="mb-3"><label class="form-label">MyTitle</label><input autofocus="" class="form-control" id="id_MyName" list="id_MyNameOptions" name="MyName" type="text" value="MyValue"></input><datalist id="id_MyNameOptions" name="MyNameOptions"><option value="Item1"></option><option value="Item2"></option></datalist></div>""");
    }
}
