using DevExpress.AspNetCore.Reporting.WebDocumentViewer;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer.Native.Services;

namespace ControlGastosWebApp.Controllers
{
    public class CustomWebDocumentViewerController : WebDocumentViewerController
    {
        public CustomWebDocumentViewerController(
            DevExpress.AspNetCore.Reporting.WebDocumentViewer.Native.Services.IWebDocumentViewerMvcControllerService service)
            : base(service)
        {
        }
    }
}