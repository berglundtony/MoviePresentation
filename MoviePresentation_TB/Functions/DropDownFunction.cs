using MoviePresentation_TB.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Web.Helpers;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Net.Http.Headers;

namespace MoviePresentation_TB.Functions
{
    public static class DropDownFunction
    {

        public static async Task<IEnumerable<SelectListItem>> GenreDropDown()
        {
            var xarea = new List<Genre>() { };
            xarea.Clear();
            xarea.Add(new Genre { id = 0, name = " Choose genre" });
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                var baseAddress = new Uri("http://api.themoviedb.org/3/");

                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {
                    GenreRootObject model = null;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                    var task = httpClient.GetAsync("genre/movie/list?language=en-US&api_key=0bd07cd4d4166d94ef83bad8d6d24b08")
                             .ContinueWith(async (taskwithresponse) =>
                             {
                                 var response = taskwithresponse.Result;
                                 response.EnsureSuccessStatusCode();

                                 if (response.IsSuccessStatusCode)
                                 {
                                     string jsonString = await response.Content.ReadAsStringAsync();
                                     model = JsonConvert.DeserializeObject<GenreRootObject>(jsonString);
                                 }
                                 if (!response.IsSuccessStatusCode)
                                 {
                                     throw new Exception((int)response.StatusCode + "-" + response.StatusCode.ToString());
                                 }

                                 foreach (var result in model.genres)
                                 {
                                     xarea.Add(new Genre { id = result.id, name = result.name });
                                 }
                             });
                    task.Wait();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return await Task.FromResult(xarea.Select(x => new SelectListItem { Value = x.id.ToString(), Text = x.name }));
        }
    }
}