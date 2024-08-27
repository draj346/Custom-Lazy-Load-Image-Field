using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Learning.Foundation.CustomFields.Modal
{
    public static class Templates
    {
        [StructLayout(LayoutKind.Sequential, Size = 1)]
        public struct Image
        {
            public static readonly ID ID = ID.Parse("{4936DD39-E20B-45C7-97DF-D32000263C4B}");

            [StructLayout(LayoutKind.Sequential, Size = 1)]
            public struct Fields
            {
                public static ID Image { get; } = new ID("{63B90E4B-B7EB-478B-A9A7-755CC5F26AEF}");

                public static ID TargetUrl { get; } = new ID("{B994F2D5-0256-432A-825C-5976D18380EC}");

                public static ID ImageCaption { get; } = new ID("{F4AD3B0C-EA55-444E-978C-D966EFA81E8E}");
            }
        }
    }
}