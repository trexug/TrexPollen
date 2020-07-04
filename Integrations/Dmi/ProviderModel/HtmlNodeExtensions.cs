using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrexPollen.Integrations.Dmi.ProviderModel
{
	public static class HtmlNodeExtensions
	{
		public static HtmlNode SingleChild(this HtmlNode node, string name)
		{
			return node.ChildNodes.Single(c => string.Equals(c.Name, name, StringComparison.InvariantCultureIgnoreCase));
		}

		public static IEnumerable<HtmlNode> Children(this HtmlNode node, string name)
		{
			return node.ChildNodes.Where(c => string.Equals(c.Name, name, StringComparison.InvariantCultureIgnoreCase));
		}

		public static int InnerTextToInt(this HtmlNode node)
		{
			return int.Parse(node.InnerText);
		}
	}
}
