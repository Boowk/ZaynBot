namespace ZaynBot.Entidades.Rpg
{
    public class Emprego
    {
        public string Nome { get; }
        public int Nivel { get; }

        public Emprego(string nome, int nivel = 1)
        {
            Nome = nome;
            Nivel = nivel;
        }
    }
}
