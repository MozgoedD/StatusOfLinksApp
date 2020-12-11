using System;
using System.Collections.Generic;
using System.Text;
using Task1.Classes;

namespace Task1.Models
{
    static class LinkFactory
    {
        static public Link CreateLink(string Uri, int Nesting, Settings Settings)
        {
            return new Link(Uri, Nesting, Settings);
        }
    }
}
