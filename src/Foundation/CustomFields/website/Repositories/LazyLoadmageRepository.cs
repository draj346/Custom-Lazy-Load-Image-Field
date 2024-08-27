using Learning.Foundation.CustomFields.Modal;
using Sitecore.Data.Fields;
using Sitecore.XA.Feature.Media.Repositories;
using Sitecore.XA.Foundation.IoC;
using Sitecore.XA.Foundation.Multisite.LinkManagers;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;
using Sitecore.XA.Foundation.RenderingVariants.Repositories;

namespace Learning.Foundation.CustomFields.Repositories
{
    public class LazyLoadmageRepository : VariantsRepository,
    IImageRepository,
    IModelRepository,
    IControllerRepository,
    IAbstractRepository<IRenderingModelBase>
    {
        public override IRenderingModelBase GetModel()
        {
            ImageRenderingModel model = new ImageRenderingModel();
            FillBaseProperties(model);
            model.Href = (dataSource, linkFieldName) =>
            {
                string targetUrl = new LinkItem((LinkField)Rendering.DataSourceItem.Fields[Templates.Image.Fields.TargetUrl]).TargetUrl;
                return string.IsNullOrWhiteSpace(targetUrl) ? "#" : targetUrl;
            };
            return model;
        }
    }
}