namespace Web.Services
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;



    public class ApiServices
    {        
        public async Task<ResponseService> GetList<T>(string urlBase, string Prefijo, string Controller, string Filtros = "")
        {
            try
            {
                string p_url = urlBase + Prefijo + Controller;
                var uri = new Uri(Path.Combine(p_url, Filtros == "" ? "Get" : Filtros));

                var Client = new HttpClient();
                var reponse = Client.GetAsync(uri).Result;
                var answer = await reponse.Content.ReadAsStringAsync();
                if (!reponse.IsSuccessStatusCode)
                {
                    return new ResponseService
                    {
                        Successful = false,
                        Message = answer,
                    };

                }

                object list;
                if (Filtros == "")
                {
                    list = JsonConvert.DeserializeObject<List<T>>(answer);
                }
                else
                {
                    list = JsonConvert.DeserializeObject<T>(answer);
                }
                return new ResponseService
                {
                    Successful = true,
                    Result = list,
                };

            }
            catch (Exception ex)
            {
                return new ResponseService
                {
                    Successful = false,
                    Message = ex.Message,
                };
            }

        }


        public async Task<ResponseService> GetListByID<T>(string urlBase, string Prefijo, string Controller, int ID = 0)
        {
            try
            {
                string p_url = urlBase + Prefijo + Controller;
                var uri = new Uri(Path.Combine(p_url, "GetByID/" + ID.ToString()));
                var Client = new HttpClient();
                var reponse = Client.GetAsync(uri).Result;
                var answer = await reponse.Content.ReadAsStringAsync();
                if (!reponse.IsSuccessStatusCode)
                {
                    return new ResponseService
                    {
                        Successful = false,
                        Message = answer,
                    };
                }

                object obj;
               
                    obj = JsonConvert.DeserializeObject<T>(answer);
               
                return new ResponseService
                {
                    Successful = true,
                    Result = obj,
                };

            }
            catch (Exception ex)
            {
                return new ResponseService
                {
                    Successful = false,
                    Message = ex.Message,
                };
            }

        }

        public T Save<T>(string urlBase, string Prefijo, string Controller, string Action, T model)
        {


            try
            {
                string url = $"{urlBase}{Prefijo}/{Controller}/{Action}";

                var Client = new HttpClient();
                var uri = new Uri(Path.Combine(url));
                var json = JsonConvert.SerializeObject(model);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseTask;
                

                if (Action == "Add"){  responseTask = Client.PostAsync(uri, data).Result;  } else { responseTask = Client.PutAsync(uri, data).Result; }
             

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsStringAsync().Result;
                    JObject jsonn = JObject.Parse(readTask.ToString());
                    model = JsonConvert.DeserializeObject<T>(jsonn["model"].ToString());
                }
                else //web api sent error response 
                {
                    //log response status here..

                   //System.Web.WebPages.Html.ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                
                return model;

            }
            catch {}
            {
               
                return model;
            }

        }




        public string SendEmail(string urlBase, string Prefijo, string Controller, string Action, object  model)
        {
            var message = "";

            try
            {
                string url = $"{urlBase}{Prefijo}/{Controller}/{Action}";

                var Client = new HttpClient();
                var uri = new Uri(Path.Combine(url));
                var json = JsonConvert.SerializeObject(model);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseTask;

                responseTask = Client.PostAsync(uri, data).Result; 

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsStringAsync().Result;
                    message = JsonConvert.DeserializeObject<string>(readTask.ToString());
                }


                return message;

            }
            catch { }
            {

                return message;
            }

        }


    }
}