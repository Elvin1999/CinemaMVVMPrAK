using CinemaMVVM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMVVM.Services
{
    public class MovieService
    {
        public static dynamic Data { get; set; }
        public static dynamic SingleData { get; set; }
        public async static Task<List<Movie>> GetMoviesAsync(string movie)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();

            response = client.GetAsync($@"http://www.omdbapi.com/?apikey=ddee1dae&s={movie}&plot=full").Result;

            var str= response.Content.ReadAsStringAsync().Result;
            Data = JsonConvert.DeserializeObject(str);
            List<Movie> movies=new List<Movie>();

            try
            {
                for (int i = 0; i < 8; i++)
                {
                    response= await client.GetAsync($@"http://www.omdbapi.com/?apikey=ddee1dae&t={Data.Search[i].Title}&plot=full");
                    str = await response.Content.ReadAsStringAsync();
                    SingleData = JsonConvert.DeserializeObject(str);
                    var mymovie = new Movie
                    {
                        Description = SingleData.Plot,
                        ImagePath = SingleData.Poster,
                        Name = SingleData.Title,
                        Rating = SingleData.imdbRating,
                    };
                    movies.Add(mymovie);
                }
            }
            catch (Exception)
            {
            }
            return movies;
        }

    }
}

