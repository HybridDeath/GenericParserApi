using System;
using System.Collections.Generic;
using System.Text;

namespace GenericParserApi.Parsers
{
    public interface IContentParser
    {
        object Parse(string content);
    }
}
