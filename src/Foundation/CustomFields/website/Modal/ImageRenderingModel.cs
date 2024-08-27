using Sitecore.Data.Items;
using Sitecore.XA.Foundation.Variants.Abstractions.Models;
using System;

namespace Learning.Foundation.CustomFields.Modal
{
    public class ImageRenderingModel : VariantsRenderingModel
    {
        public Func<Item, string, string> Href { get; set; }
    }
}