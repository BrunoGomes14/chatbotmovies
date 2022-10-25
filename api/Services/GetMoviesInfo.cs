﻿using api.Models;
using api.Models.Exception;
using api.Services.External;
using System.Text;

namespace api.Services
{
    public class GetMoviesInfo
    {
        private readonly ITmdbAPI _tmbdbAPI;
        private readonly string _apiKey;

        public GetMoviesInfo(ITmdbAPI tmbdbAPI, AppSettings settings)
        {
            _tmbdbAPI = tmbdbAPI;
            _apiKey = settings.TmdbKey;
        }

        public async Task<TmdbMovieResult> SearchMovie(string movieSearch)
        {
            var search = await _tmbdbAPI.FindMovieByName(movieSearch, _apiKey);
            if (!search.results.Any())
            {
                throw new NotUnderstandException(Messages.MovieNotFound);
            }


            bool isTrust = true;
            var movie = search.results.FirstOrDefault(x => x.title.ToLower().Trim() == movieSearch.ToLower().Trim());
            if (movie == null)
            {
                isTrust = false;
                movie = search.results.FirstOrDefault(x => x.title.ToLower().Contains(movieSearch.ToLower()));
                if (movie == null)
                {
                    movie = search.results[0];
                }
            }

            return await GetMovie(movie.id, isTrust);
        }

        public async Task<TmdbMovieResult> GetMovie(int id, bool isTrust = true)
        {
            var movie = await _tmbdbAPI.GetMovieDetail(id, _apiKey);
            var genres = await _tmbdbAPI.GetGenres(_apiKey);
            var stream = await _tmbdbAPI.GetMovieStream(id, _apiKey);

            return new TmdbMovieResult
            {
                movie = movie,
                stream = stream,
                isTrust = isTrust
            };
        }

        public string FormatResult(TmdbMovieResult result)
        {
            int phrase = new Random().Next(0, InicialPhrases().Count - 1);

            StringBuilder sb = new();
            sb.Append(InicialPhrases()[phrase]);
            sb.Append("\n\n");
            
            sb.Append($"🎥*{result.movie.title}*\n");
            sb.Append(result.movie.overview);
            
            sb.Append($"\n\n⏱ {result.movie.runtime} minutos\n");
            
            sb.Append($"📅 Lançado em {result.movie.release_date.ToString("dd/MM/yyyy")}\n");
            
            sb.Append($"🏆 Nota média {Math.Round(result.movie.vote_average, 1)}\n");
            
            sb.Append("🍿*Gêneros*\n");
            result.movie.genres.ForEach(x => sb.Append($"   · {x.name}\n"));
            
            if (result.stream.results.BR != null && result.stream.results.BR.flatrate.Any())
            {
                sb.Append("\n🖥 Disponível nos streams:\n");
                result.stream.results.BR.flatrate.ForEach(x => sb.Append($"   · {x.provider_name}\n"));
            }
            
            return sb.ToString();
        }

        public List<string> InicialPhrases() => new List<string>
            { 
                "É vapt vupt! Tá aqui um pouquinho sobre o filme 😀",
                "Gente vamos maratornar?👀 Filme fresquinho....",
                "Olha que filmão! Tá aqui um pouquinho sobre ele",
                "Achei o filme que você me pediu! Tá aqui um pouquinho sobre ele",
            };
    }
}