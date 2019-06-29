using System;

namespace ZaynBot.RPG.Exceptions
{
    public class PersonagemNullException : Exception
    {
        public override string ToString()
        {
            return "você precisa criar um personagem com o comando z!reencarnar";
        }
    }
}
