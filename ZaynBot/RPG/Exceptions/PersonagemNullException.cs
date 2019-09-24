﻿namespace ZaynBot.RPG.Exceptions
{
    public class PersonagemNullException : MensagemException
    {
        public PersonagemNullException()
            => Mensagem = "você precisa criar um personagem com o comando z!reencarnar";

        public PersonagemNullException(string mensagem) : base(mensagem) { }
    }
}
