using api.Models;
using api.Services;
using System.Globalization;
using System.Text;
using System.Text.Encodings;

namespace api.Business
{
    public class TranslateAnswer
    {
        private string _content;
        private List<ContentOption> _options;
        private readonly Dictionary<string, int> _digits;
        private readonly Dictionary<string, int> _ordinaryNumber;

        public const int NotFound = -1;
        public const int ExitDetected = -2;
        public const int RestartDetected = -3;

        public TranslateAnswer()
        {
            _content = "";
            _options = new List<ContentOption>();
            _digits = new Dictionary<string, int>(){
                {"um", 1},
                {"dois", 2},
                {"tres", 3},
                {"quatro", 4},
                {"cinco", 5},
                {"seis", 6},
                {"sete", 7},
                {"oito", 8},
                {"nove", 9},
                {"dez", 10},
            };

            _ordinaryNumber = new Dictionary<string, int>(){
                {"primeir", 1},
                {"segund", 2},
                {"terceir", 3},
                {"quart", 4},
                {"quint", 5},
                {"sext", 6},
                {"setim", 7},
                {"oitav", 8},
                {"non", 9},
                {"decim", 10},
            };
        }

        public void Setup(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                content = "";
                return;
            }

            _content = content.RemoveAccents().ToLower();
        }

        public int ToListOption(List<ContentOption> options, bool verifyExit = false)
        {
            _options = options;

            if (verifyExit)
            {
                verifyExit = LookForGiveUp();
                if (verifyExit)
                    return ExitDetected;
            }

            int digit = LookForDigit();

            if (digit == NotFound)
            {
                digit = LookForVerb();   
            }

            return digit;
        }

        private int LookForDigit()
        {
            int number = 0;
            bool isNumber = false;
            string wordSplited = "";

            if (!int.TryParse(_content, out number) && _content.Any(char.IsDigit))
            {
                foreach (string word in _content.Split(' '))
                {
                    wordSplited = word.Trim();
                    if (string.IsNullOrEmpty(wordSplited))
                        continue;

                    if (_content.Length > 0 && int.TryParse(wordSplited, out number))
                        break;

                }
            }
            else
            {
                foreach (string word in _content.ToLower().RemoveAccents().Split(' '))
                {
                    wordSplited = word.Trim();
                    if (string.IsNullOrEmpty(wordSplited))
                        continue;

                    bool isOrdinary = _ordinaryNumber.ContainsKey(wordSplited.Substring(0, wordSplited.Length - 1));
                    if (isOrdinary)
                    {
                        number = _ordinaryNumber[wordSplited.Substring(0, wordSplited.Length - 1)];
                        break;
                    }       
                }
            }
            
            isNumber = number != 0;


            if (isNumber)
                return _options.FindIndex(x => x.NumberOption == number);
            else 
                return NotFound;
        }

        private int LookForVerb()
        {
            var option = _options.FirstOrDefault(x => x.VerbsOptions.FirstOrDefault(x => _content.Contains(x)) != null);
            return option != null ? _options.IndexOf(option) : -1;
        }

        public string? GetCEPInText(string? cep)
        {
            List<string> itens = cep!.Replace("-", "")
                        .Replace(".", "")
                        .Split(' ')
                        .ToList();
            cep = null;
            double result = 0;
            if (itens.Any(x => x.Length == 8) && double.TryParse(itens.First(x => x.Length == 8), out result))
            {
                cep = itens.First(x => x.Length == 8);
            }

            return cep;
        }

        public int CheckAnyTimeDecision()
        {
            bool result = LookForGiveUp();

            if (result)
                return ExitDetected;

            result = LookForRestart();

            if (result)
                return RestartDetected;

            return NotFound;
        }

        private bool LookForGiveUp() => 
            new List<string>() { 
                "desisto",
                "finalizar",
                "sair",
                "parar"
            }.Any(x => x == _content);

        private bool LookForRestart() =>
            new List<string>() {
                        "comecar de novo",
                        "re",
                        "reiniciar",
                        "restart"
            }.Any(x => x == _content);
    }
}