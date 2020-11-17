using System;
using System.Collections.Generic;
using System.Text;

namespace Task1
{
    static class LinkFactory
    {
        static public Link CreateLink(string Uri, int Nesting, Settings Settings)
        {
            return new Link(Uri, Nesting, Settings);
        }
    }
}
