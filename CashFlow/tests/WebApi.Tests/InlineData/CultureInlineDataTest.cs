using System.Collections;
using System.Globalization;

namespace WebApi.Tests.InlineData;
public class CultureInlineDataTest : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "pt-BR" };
        yield return new object[] { "pt-PT" };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
