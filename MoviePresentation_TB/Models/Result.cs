using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;

namespace MoviePresentation_TB.Models
{

    public class Result
    {
        public int vote_count { get; set; }
        public int id { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public string title { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public List<int> genre_ids { get; set; }
        public string backdrop_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        //public List<Result> _ResultList { get; set; }
        //public Result()
        //{
        //    this._ResultList = new List<Result> { };
        //}
    }
  


    public class Genre
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        public int? GenreID { get; set; }
        public IEnumerable<SelectListItem> GenreDrop { get; set; }
        public IEnumerable<Result> _ResultList { get; set; }
        public Genre()
        {
            this._ResultList = new List<Result> { };
            GenreDrop = new List<SelectListItem>();
        }
    }

    public class GenreRootObject
    {
        public List<Genre> genres { get; set; }
    }

    public class RootObject
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Result> results { get; set; }
    }
}