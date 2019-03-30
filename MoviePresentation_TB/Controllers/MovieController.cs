using JsAction;
using MoviePresentation_TB.Functions;
using MoviePresentation_TB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MoviePresentation_TB.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        IEnumerable<Result> _movielist = new List<Result>();

        public async Task<ActionResult> Index()
        {
            var model = new Genre();
            model._ResultList = await MovieFunction.GetMoviesByGenres(28);
            model.GenreDrop = await DropDownFunction.GenreDropDown();
           
            return View(model);
        }
 
        public async Task<PartialViewResult> GenresChoosenItem(int? GenreID)
        {
            var model = new Genre();
            model._ResultList = await MovieFunction.GetMoviesByGenres(GenreID);
            return PartialView(model._ResultList);
        }

    }
}
