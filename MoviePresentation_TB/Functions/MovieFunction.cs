using MoviePresentation_TB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace MoviePresentation_TB.Functions
{
    public static class MovieFunction
    {
        public static async Task<IEnumerable<Result>> GetMoviesByGenres(int? GenreID)
        {
            var model = new RootObject();
            var genre = new Genre();
            var moviearea = new List<Result>{ };
      
                genre.GenreDrop = await DropDownFunction.GenreDropDown();

            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                var baseAddress = new Uri("http://api.themoviedb.org/3/");

                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {
                  

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                    var url = string.Format("discover/movie?with_genres={0}&language=en-US&sort_by = revenue.desc&api_key=0bd07cd4d4166d94ef83bad8d6d24b08", GenreID);

                    var task = httpClient.GetAsync(url)
                             .ContinueWith(async (taskwithresponse) =>
                             {
                                 var response = taskwithresponse.Result;
                                 response.EnsureSuccessStatusCode();

                                 if (response.IsSuccessStatusCode)
                                 {
                                     string jsonString = await response.Content.ReadAsStringAsync();
                                     model = JsonConvert.DeserializeObject<RootObject>(jsonString);
                                 }
                                 if (!response.IsSuccessStatusCode)
                                 {
                                     throw new Exception((int)response.StatusCode + "-" + response.StatusCode.ToString());
                                 }
                                 foreach (var result in model.results)
                                 {
                                     moviearea.Add(new Result { vote_count = result.vote_count, video = result.video, vote_average = result.vote_average, title= result.title, popularity=result.popularity, poster_path = "https://image.tmdb.org/t/p/w500/" + result.poster_path, original_language=result.original_language, original_title = result.original_title, backdrop_path = "https://image.tmdb.org/t/p/w500/" + result.backdrop_path, adult = result.adult, overview = result.overview, release_date= result.release_date });
                                 }

                             });
                    task.Wait();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return await Task.Run(() => moviearea.AsEnumerable());
        }
    }
}