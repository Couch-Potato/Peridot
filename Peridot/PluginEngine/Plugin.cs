using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peridot.PluginEngine
{
    class Plugin
    {
        PeridotHttpServer peridot;
        Session s;
        public enum PluginArrayScope { ConnectionHandler, Startup, Both, None};
    }
    public class HttpResponse
    {
        public enum PeridotHttpStatus { Unauthorized, NotFound, Forbidden, InternalServerError,Success };
        public string httpResponse;
        public HttpResponse(PeridotHttpStatus status, string response)
        {
            switch (status)
            {
                case (PeridotHttpStatus.Unauthorized):
                    httpResponse = Peridot.Config.ErrorConfig.Error401;
                    break;
                case (PeridotHttpStatus.Forbidden):
                    httpResponse = Peridot.Config.ErrorConfig.Error403;
                    break;
                case (PeridotHttpStatus.NotFound):
                    httpResponse = Peridot.Config.ErrorConfig.Error404;
                    break;
                case (PeridotHttpStatus.InternalServerError):
                    httpResponse = Peridot.Config.ErrorConfig.Error500;
                    break;
                case (PeridotHttpStatus.Success):
                    httpResponse = response;
                    break;
            }
        }
    }
}
