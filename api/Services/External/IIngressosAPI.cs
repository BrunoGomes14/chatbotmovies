using api.Models;
using Refit;

namespace api.Services.External
{
    public interface IIngressosAPI
    {
        [Get("/states/{state}")]
        Task<IngresssoStates> GetCitiesState(string state);

        [Get("/theaters/city/{cityId}/partnership/-")]
        Task<IngressoTheater> GetCityTheaters(string cityId);

        [Get("/sessions/city/{cityId}/theater/{theaterId}/partnership/-")]
        Task<IEnumerable<IngressoMovie>> GetTheaterMovies(string cityId, string theaterId);
    }
}
