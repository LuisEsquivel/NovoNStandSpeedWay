using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public class Response
    {

        public object ResponseValues(int StatusCode,
                                      object Model = null,
                                      string Message = ""
                                     )
        {


            object json = new
            {
                statuscode = StatusCode,
                status = StatusCode == 200 ? "success" : "error",
                message = Message,
                model = Model
            };

            return JsonConvert.SerializeObject(json);

        }

    }
}
