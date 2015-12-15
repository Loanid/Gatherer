using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GathererDB
{
    internal static class ExtensionMethods
    {
        internal static IEnumerable<HtmlNode> GetNodesByName(this HtmlNode oNode, string sName)
        {
            foreach (HtmlNode oChild in oNode.ChildNodes)
            {
                if (oChild.Name == sName)
                    yield return oChild;

                foreach (HtmlNode oChildofChild in oChild.GetNodesByName(sName))
                {
                    yield return oChildofChild;
                }
            }
        }
    }
}
