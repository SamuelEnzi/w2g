using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w2g.core.Models.Base;

namespace w2g.core
{
    public static class Parser
    {
        public static (object? data, RequestType type)? Parse(this string request)
        {
            //json
            try
            {
                var type = request.Deserialize<Models.Base.Request>();
                if (type == null) return null;

                switch (type.RequestType)
                {
                    case Models.Base.RequestType.VideoSet:
                        return (request.Deserialize<Models.UrlModel>(), type.RequestType);
                    case Models.Base.RequestType.TimeSet:
                        return (request.Deserialize<Models.TimeModel>(), type.RequestType);
                    case Models.Base.RequestType.Play:
                        return (request.Deserialize<Models.PlayModel>(), type.RequestType);
                    case Models.Base.RequestType.Stop:
                        return (request.Deserialize<Models.StopModel>(), type.RequestType);
                    case Models.Base.RequestType.Current:
                        return (request.Deserialize<Models.CurrentModel>(), type.RequestType);
                    case Models.Base.RequestType.None:
                        return null;
                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
