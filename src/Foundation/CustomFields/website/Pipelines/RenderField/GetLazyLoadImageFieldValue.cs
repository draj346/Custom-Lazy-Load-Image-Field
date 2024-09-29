using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Pipelines.RenderField;
using Sitecore.Xml.Xsl;

namespace Learning.Foundation.Pipelines.RenderField
{
    public class GetLazyLoadImageFieldValue
    {
        protected virtual ImageRenderer CreateRenderer() => new ImageRenderer();
        private static readonly string _TitleFieldName = "title";
        protected virtual string TitleFieldName => _TitleFieldName;

        public void Process(RenderFieldArgs args)
        {
            if (args != null && (args.FieldTypeKey == "lazy load image"))
            {
                ImageRenderer renderer = CreateRenderer();
                ConfigureRenderer(args, renderer);
                SetRenderFieldResult(renderer.Render(), args);
                string image = args.Result.FirstPart;

                if (!CanRenderField(args))
                {
                    image = image.Replace("src", "data-src");
                    image = image.Insert(image.Length - 3, " data-class=\"lozad\"");
                    args.Result.FirstPart = image;
                }
            }
        }
        protected virtual void ConfigureRenderer(RenderFieldArgs args, ImageRenderer imageRenderer)
        {
            Item itemToRender = args.Item;
            imageRenderer.Item = itemToRender;
            imageRenderer.FieldName = args.FieldName;
            imageRenderer.FieldValue = args.FieldValue;
            imageRenderer.Parameters = args.Parameters;
            if (itemToRender == null)
                return;
            imageRenderer.Parameters.Add("la", itemToRender.Language.Name);
            EnsureMediaItemTitle(args, itemToRender, imageRenderer);
        }

        protected virtual void EnsureMediaItemTitle(RenderFieldArgs args, Item itemToRender, ImageRenderer imageRenderer)
        {
            if (!string.IsNullOrEmpty(args.Parameters[TitleFieldName]))
                return;
            Item innerImageItem = GetInnerImageItem(args, itemToRender);
            if (innerImageItem == null)
                return;
            Field field = innerImageItem.Fields[TitleFieldName];
            if (field == null)
                return;
            string str = field.Value;
            if (string.IsNullOrEmpty(str) || imageRenderer.Parameters == null)
                return;
            imageRenderer.Parameters.Add(TitleFieldName, str);
        }

        protected virtual Item GetInnerImageItem(RenderFieldArgs args, Item itemToRender)
        {
            Field field = itemToRender.Fields[args.FieldName];
            return field == null ? null : new ImageField(field, args.FieldValue).MediaItem;
        }


        protected virtual void SetRenderFieldResult(RenderFieldResult result, RenderFieldArgs args)
        {
            args.Result.FirstPart = result.FirstPart;
            args.Result.LastPart = result.LastPart;
            args.WebEditParameters.AddRange(args.Parameters);
            args.DisableWebEditContentEditing = true;
            args.DisableWebEditFieldWrapping = true;
            args.WebEditClick = "return Sitecore.WebEdit.editControl($JavascriptParameters, 'webedit:chooseimage')";
        }

        public static bool CanRenderField(RenderFieldArgs args)
        {
            if((!args.WebEditParameters.ContainsKey("customData") && !args.WebEditParameters.ContainsKey("contextItem") && !Sitecore.Context.PageMode.IsExperienceEditor) || args.Item == null )
                return false;
            return true;
        }
    }
}