using api.Models;
using api.Services.External;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace api.Services
{
    public class GetNearestTheater
    {
        private readonly IGeolocationAPI _geolocationAPI;
        private readonly IIngressosAPI _ingressosAPI;
        private readonly string _googleKey;

        public GetNearestTheater(
            IGeolocationAPI geolocationAPI,
            IIngressosAPI ingressosAPI,
            AppSettings settings)
        {
            _geolocationAPI = geolocationAPI;
            _ingressosAPI = ingressosAPI;
            _googleKey = settings.GoogleKey;
        }

        public async Task<NearestTheaterResult> Execute(decimal latitude, decimal longitude, string? cep)
        {
            GeolocationModel result = new();
            if (latitude != 0)
                result = await _geolocationAPI.GetUserLocation(latitude, longitude, _googleKey);
            else
                result = await _geolocationAPI.GetUserCepLocation(cep!, _googleKey);
            
            var state = result.results[0].address_components.First(x => x.types.Contains("administrative_area_level_1"));
            var city = result.results[0].address_components.First(x => x.types.Contains("administrative_area_level_2")); ;
            latitude = result.results[0].geometry.location.lat;
            longitude = result.results[0].geometry.location.lng;
            
            var cities = await _ingressosAPI.GetCitiesState(state.short_name);
            
            var ingressoCity = cities.cities.FirstOrDefault(x => x.name.ToLower() == city.long_name.ToLower());
            if (ingressoCity == null)
            {
                new NearestTheaterResult("Infelizmente, não consegui encontrar cinemas na sua cidade 😢", false);
            }
            
            var theaters = await _ingressosAPI.GetCityTheaters(ingressoCity.id);
            var theatersFiltred = theaters.items.Where(x => x.geolocation.lng != 0
                                                         && x.enabled);
            
            GeolocationDistanceModel distance;
            foreach (var item in theatersFiltred)
            {
                distance = await _geolocationAPI.GetComparation(item.geolocation.lat,
                                                                item.geolocation.lng,
                                                                latitude,
                                                                longitude,
                                                                _googleKey);
                
                item.Distance = distance.rows[0].elements[0].distance.value;
            }
            
            var theaterNear = theatersFiltred.OrderBy(x => x.Distance).ToList()[0];
            
            //var theaterNear = JsonConvert.DeserializeObject<Item>(cinemakSpMarket());
            
            var movies = await _ingressosAPI.GetTheaterMovies(theaterNear!.cityId, theaterNear.id);

            return new NearestTheaterResult()
            {
                Sucess = true,
                MoviesAvaible = movies.ToList(),
                Theater = theaterNear
            };
        }

        public string FormatResult(NearestTheaterResult theaterResult)
        {
            var sb = new StringBuilder();
            sb.Append("Consegui achar!🎬\n\n");
            sb.Append($"O cinema mais perto de você é o\n*{theaterResult.Theater.name}*\n\n");
            sb.Append($"Ele está localizado em:\n{theaterResult.Theater.address}, {theaterResult.Theater.number}\n");
            sb.Append($"no bairro {theaterResult.Theater.neighborhood}.\n");
            sb.Append($"*Há {theaterResult.Theater.totalRooms} salas* no momento.\n\n");


            if (theaterResult.MoviesAvaible.Any())
            {
                var secao = theaterResult.MoviesAvaible[0];

                sb.Append($"*Tá querendo ver um filme?*\nEntão fica ligado que essas são as próximas seções\n");
                sb.Append($"📅 {secao.dateFormatted}\n({secao.dayOfWeek})\n\n");

                foreach (var movie in secao.movies.Take(5))
                {
                    sb.Append($"🎥 {movie.title}\n");
                    sb.Append($"⏱ {movie.duration} minutos\n");

                    sb.Append(RatingEmoji(movie.contentRating));
                    sb.Append($" {movie.contentRating}\n");

                    if (movie.rottenTomatoe != null)
                    {
                        sb.Append($"🍅 Score {movie.rottenTomatoe.criticsScore}\n");

                    }

                    sb.Append("🍿*Gêneros*\n");
                    foreach (var item in movie.genres)
                    {
                        sb.Append($"   · {item}\n");
                    }

                    if (movie.rooms.Count > 0)
                    {
                        var nextSessionRoom = movie.rooms[0];
                        var nextSession = nextSessionRoom.sessions[0];

                        sb.Append("\nPróxima seção\n");
                        sb.Append($"📆 {nextSession.date.dayAndMonth}\n");

                        TimeOnly time = TimeOnly.Parse(nextSession.date.hour);
                        time = time.AddMinutes(Convert.ToInt32(movie.duration));

                        sb.Append($"⌚ {nextSession.date.hour} ~ {time}\n");
                        sb.Append($"🔈 {nextSession.type[0]}\n");
                        sb.Append($"🚪 {nextSession.room}\n");
                        sb.Append("\n");
                    }
                }

                sb.Append("Para mais informações você pode acessar\n");
                sb.Append(theaterResult.Theater.siteURL);
            }
            else
            {
                sb.Append("Não achei nenhuma seção pra esse cinema. 😨\n");
                sb.Append("Mas de qualquer forma vou deixar aqui o site para mais informações");
                sb.Append(theaterResult.Theater.siteURL);
            }

            return sb.ToString();
        }

        private string RatingEmoji(string content) => content switch
        {
            "Livre" => "🟢",
            "18 anos" => "⚫",
            "14 anos" => "🟠",
            "16 anos" => "🔴",
            _ => ""
        };

        public string cinemakSpMarket() =>
            @"
            {
                ""id"": ""378"",
                ""images"": [
                {
                    ""url"": ""https://ingresso-a.akamaihd.net/catalogo/img/exibidores/cinema/cinemark.jpg"",
                    ""type"": ""Logo""
                }
                ],
                ""urlKey"": ""cinemark-sp-market"",
                ""name"": ""Cinemark SP Market"",
                ""siteURL"": ""https://www.ingresso.com/cinema/cinemark-sp-market?city=sao-paulo&partnership=a?ing_source=api&ing_medium=link-cinema&ing_campaign=a&ing_content="",
                ""nationalSiteURL"": ""https://www.ingresso.com/cinema/cinemark-sp-market?city=sao-paulo&partnership=a?ing_source=api&ing_medium=link-cinema&ing_campaign=a&ing_content="",
                ""cnpj"": ""00779721000656"",
                ""districtAuthorization"": ""26704641"",
                ""address"": ""Av. das Nações Unidas"",
                ""addressComplement"": ""Bloco C"",
                ""number"": ""22540 "",
                ""cityId"": ""1"",
                ""cityName"": ""São Paulo"",
                ""state"": ""São Paulo"",
                ""uf"": ""SP"",
                ""neighborhood"": ""Campo Grande"",
                ""properties"": {
                ""hasBomboniere"": true,
                ""hasContactlessWithdrawal"": true,
                ""hasSession"": true,
                ""hasSeatDistancePolicy"": false,
                ""hasSeatDistancePolicyArena"": false
                },
                ""functionalities"": {
                ""operationPolicyEnabled"": true
                },
                ""telephones"": [
                ""(11) 56862595""
                ],
                ""geolocation"": {
                ""lat"": -23.67836,
                ""lng"": -46.6989
                },
                ""deliveryType"": [
                ""Bilheteria/ATM"",
                ""Aplicativo/Scannerless""
                ],
                ""corporation"": ""Cinemark"",
                ""corporationId"": ""1"",
                ""corporationPriority"": 0,
                ""corporationAvatarBackground"": ""#a53336"",
                ""rooms"": [],
                ""totalRooms"": 11,
                ""enabled"": true,
                ""blockMessage"": ""Infelizmente estamos sem comunicação com o local."",
                ""partnershipType"": null,
                ""operationPolicies"": []
            }";
    }
}
