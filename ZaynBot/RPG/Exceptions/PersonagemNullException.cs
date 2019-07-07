using System;

namespace ZaynBot.RPG.Exceptions
{
    public class PersonagemNullException : Exception
    {
        public string Mensagem { get; set; }

        public PersonagemNullException()
        {
            Mensagem = "você precisa criar um personagem com o comando z!reencarnar";
        }

        public PersonagemNullException(string mensagem)
        {
            Mensagem = mensagem;
        }

        public override string ToString()
        {
            return Mensagem;
        }
    }
}
