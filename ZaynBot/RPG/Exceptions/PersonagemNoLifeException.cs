namespace ZaynBot.RPG.Exceptions
{
    public class PersonagemNoLifeException : MensagemException
    {
        public PersonagemNoLifeException()
            => Mensagem = "você está sem vida para efetuar ações.";

        public PersonagemNoLifeException(string mensagem) : base(mensagem) { }
    }
}
