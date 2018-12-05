using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UploadImage.Startup))]
namespace UploadImage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
