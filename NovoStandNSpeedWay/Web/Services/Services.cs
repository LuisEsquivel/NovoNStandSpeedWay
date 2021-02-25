using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Novosys.Services
{


        public class Services : Controller
        {



        public List<T> Get<T>(string entity, string MetaTagProducto = "")
        {
            List<T> objeto = null;

            var method = "/get";
            if (MetaTagProducto.Trim().Length > 0) method = "/GetByMetaTagProducto/" + MetaTagProducto;
            //var url = CoreResources.UrlBase + CoreResources.Prefix + "/" + entity + method;

            try
            {

                using (var client = new HttpClient())
                {
                    //var uri = new Uri(Path.Combine(baseAddress, entity, "Get"));

                    //var uri = new Uri(Path.Combine(url));

                    //var responseTask = client.GetAsync(uri).Result;

                    //if (responseTask.IsSuccessStatusCode)
                    //{
                    //    string readTask = responseTask.Content.ReadAsStringAsync().Result;
                    //    objeto = JsonConvert.DeserializeObject<List<T>>(readTask);

                    //}
                    //else //web api sent error response 
                    //{
                    //    //log response status here..

                    //    objeto = null;


                    //    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    //}
                }



                return objeto;

            }
            catch (Exception) 
            {
                return objeto;
            }

        }






        public object Login<T>(T model)
        {
            object objeto = null;


        //var url = CoreResources.UrlBase + CoreResources.Prefix + "/usuario/login";

            try
            {

                using (var client = new HttpClient())
                {
      
                    //var uri = new Uri(Path.Combine(url));
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    //var responseTask = client.PostAsync(uri, content).Result;

                    //if (responseTask.IsSuccessStatusCode)
                    //{
                    //    string readTask = responseTask.Content.ReadAsStringAsync().Result;
                    //    JObject jsonn = JObject.Parse(readTask.ToString()); 
                    //    objeto = jsonn.Value<object>("token").ToString();
                    //}
                    //else 
                    //{

                    //    objeto = null;

                    //    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    //}
                }


                return objeto;

            }
            catch (Exception)
            {
                return objeto;
            }

        }



    }
            
}


   

    