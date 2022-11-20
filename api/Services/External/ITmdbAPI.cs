using api.Models;
using Refit;

namespace api.Services.External
{
    public interface ITmdbAPI
    {
        [Get("/search/movie?query={movieName}&api_key={key}&language=pt-BR")]
        public Task<TbmdbSearchResult> FindMovieByName(string movieName, string key);

        [Get("/movie/{id}?api_key={key}&language=pt-BR")]
        public Task<TmdbMovie> GetMovieDetail(int id, string key);

        [Get("/genre/movie/list?api_key={key}&language=pt-BR")]
        public Task<TmdbGenres> GetGenres(string key);

        [Get("/movie/{id}/watch/providers?api_key={key}")]
        public Task<TmdbStream> GetMovieStream(int id, string key);

        [Get("/movie/popular?api_key={key}&language=pt-BR")]
        public Task<TbmdbSearchResult> GetPopularMovie(string key);

    }
}
