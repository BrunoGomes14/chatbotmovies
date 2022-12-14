namespace api.Services
{
    public static class Messages
    {
        public static string StartMessage
        { 
            get
            { 
                return $"Olá {{0}} 😃\n\nEu sou a Bovie! O seu bot de movies.\nSou especialista em filmes e to sempre por dentro do mundo dos cinemas!\n\nQuer saber alguma coisa? Agora, tenho essas opções aqui:\n\n{Options}\n\nE aí o que vai ser?";
            }  
        }

        public static string RestartMessage
        {
            get
            {
                return $"Olá {{0}} 😃\n\nQuem bom te encontrar aqui de novo!\nComo posso te ajudar?\n\n{Options}";
            }
        }

        public static string Options
        {
            get
            {
                return "*1.* Encontrar filmes.\n*2.* Sortear um filme.\n*3.* Descobrir o que tá em cartaz do cinema mais próximo a você!";
            }
        }

        public static string NotUnderstand
        {
            get
            {
                return "Me desculpa mas não consegui te entender.\nPoderia tentar escrever de outra forma? 🤔";
            }
        }

        public static string Error
        {
            get
            {
                return "Ish deu problema aqui hihi. Vamos tentar de novo? Me mande outra mensagem para começarmos de novo.";
            }
        }

        public static string SendLocation
        {
            get
            {
                return "Me manda sua localização? Sem ser em tempo real viu? 📍\n\nAh caso não queira, você pode mandar seu CEP que já serve!";
            }
        }

        public static string FindingLocation
        {
            get
            {
                return "Beleza! Vou procurar aqui, deixa comigo.🧐🔎";
            }
        }
 
        public static string NotUnderstandLocation
        {
            get
            {
                return "Pera aê, não é bem o que estava espearando pode me mandar sua localização *fixa* ou seu cep?";
            }
        }


        public static string SearchMovie
        {
            get
            {
                return "Beleza! Me manda o filme que você tá querendo saber 😄";
            }
        }

        public static string MeetMovie
        {
            get
            {
                return "Quer conhecer mais sobre algum filme antes de ir ver?\nMe manda o nome dele que eu mostro!";
            }
        }

        public static string MovieNotFound 
        {
            get
            {
                return "Ops! Não achei o filme por esse nome, é esse nome mesmo?🧐";
            }
        }

        public static string NotUnderstandStatus
        {
            get
            {
                return "Minha nossa me perdi aqui 🥴. Infelizmente precisei encerrar nossa conversa.\nFaz o seguinte, me manda uma mensagem para recomeçar e fingir que isso nunca aconteceu rs. ";
            }
        }

        public static string FinishedByChoice
        {
            get
            {
                return "Poxa, tudo bem 🙁. Te espero aqui caso precise.";
            }
        }

        public static string SortMovie
        {
            get
            {
                return "Essa funciona assim:\nIrei buscar os filmes que as pessoas mais estão comentando no momento e sortear um para você conhecer!\n Vamos nessa?\n\n*1.* Sim\n*2.* Não, trocar função";
            }
        }

        public static string AfterMovieDecicions
        {
            get
            {
                return "O que deseja agora?\n\n*1.* Procurar outro filme.\n*2.* Escolher outra função.";
            }
        }
    }
}