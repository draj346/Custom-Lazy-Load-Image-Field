using Learning.Foundation.CustomFields.Repositories;
using Sitecore.XA.Foundation.Mvc.Controllers;
using Sitecore.XA.Feature.Media;
using System.Web.Mvc;
using Sitecore.XA.Feature.Media.Controllers;

namespace Learning.Foundation.CustomFields.Controller
{
    public class LazyLoadImageController : ImageController
    {
        public LazyLoadImageController(Sitecore.XA.Feature.Media.Repositories.IImageRepository imageRepository) : base(imageRepository)
        {
        }

        protected override object GetModel() => ImageRepository.GetModel();
    }
}