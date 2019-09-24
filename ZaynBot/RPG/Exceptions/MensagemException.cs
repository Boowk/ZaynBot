using System;

namespace ZaynBot.RPG.Exceptions
{
    public class MensagemException : Exception
    {
        public string Mensagem { get; set; }

        public MensagemException() { }

        public MensagemException(string mensagem)
            => Mensagem = mensagem;

        public override string ToString()
            => Mensagem;
    }
}
