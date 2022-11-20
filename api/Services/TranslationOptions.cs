using api.Models;

namespace api.Services
{
    public static class TranslationOptions
    {
        public static List<ContentOption> ChooseFuncOptions()
        {
            var list = new List<ContentOption>();
            list.Add(new ContentOption()
            {
                NumberOption = 1,
                Substantive = new List<string> { "filme" },
                VerbsOptions = new List<string>() { "procurar", "pesquisar" }

            });

            list.Add(new ContentOption()
            {
                NumberOption = 2,
                VerbsOptions = new List<string>() { "sortear", "descobrir" }
            });

            list.Add(new ContentOption()
            {
                NumberOption = 3,
                Substantive = new List<string> { "cinema", "cine", "cinemazinho" },
                VerbsOptions = new List<string>() { "buscar", "localizar", "encontrar" }
            });

            return list;
        }

        public static List<ContentOption> ChooseFuncOptionsAfterMovie()
        {
            var list = new List<ContentOption>();
            list.Add(new ContentOption()
            {
                NumberOption = 1,
                Substantive = new List<string> { "filme" },
                VerbsOptions = new List<string>() { "procurar", "pesquisar" }

            });

            list.Add(new ContentOption()
            {
                NumberOption = 2,
                VerbsOptions = new List<string>() { "adicionar" }
            });

            list.Add(new ContentOption()
            {
                NumberOption = 3,
                VerbsOptions = new List<string>() { "reiniciar" }
            });

            return list;
        }

        public static List<ContentOption> ChooseFuncOptionsSortMovie() =>
            new()
            {
                new()
                {
                    NumberOption = 1,
                    VerbsOptions = new List<string>() { "sim", "bora", "fechou", "quero" }
                },
                new()
                {
                    NumberOption = 2,
                    VerbsOptions = new List<string>() { "nao" }
                }
            };
    }
}
