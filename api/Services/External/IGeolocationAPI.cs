using api.Models;
using Refit;

namespace api.Services.External
{
    public interface IGeolocationAPI
    {
        [Get("/geocode/json?latlng={lat},{lng}&key={key}")]
        Task<GeolocationModel> GetUserLocation(decimal lat, decimal lng, string key);

        [Get("/distancematrix/json?destinations={destLat},{destLng}&origins={orgLat},{orgLng}&key={key}")]
        Task<GeolocationDistanceModel> GetComparation(decimal destLat, decimal destLng, decimal orgLat, decimal orgLng, string key);

        [Get("/geocode/json?address={cep}&key={key}")]
        Task<GeolocationModel> GetUserCepLocation(string cep, string key);
    }
}
