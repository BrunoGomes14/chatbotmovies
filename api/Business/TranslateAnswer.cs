using api.Models;
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
        }

        public int ToListOption(string content, List<ContentOption> options)
        {
            _content = content.ToLower();
            _options = options;

            int digit = LookForDigit();

            if (digit == -1)
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

            if (!int.TryParse(_content.Trim(), out number) && _content.Any(char.IsDigit))
            {
                foreach (string word in _content.Split(' '))
                {
                    wordSplited = word.Trim();

                    if (_content.Length > 0 && int.TryParse(wordSplited, out number))
                        break;
                }
            }
            
            isNumber = number != 0;

            if (isNumber)
                return _options.FindIndex(x => x.NumberOption == number);
            else 
                return -1;
        }

        private int LookForVerb()
        {
            var option = _options.FirstOrDefault(x => x.VerbsOptions.FirstOrDefault(x => _content.Contains(x)) != null);
            return option != null ? _options.IndexOf(option) : -1;
        }
    }
}