using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.Entidades.EntidadesRpg
{
    [BsonIgnoreExtraElements]
    public class Mob
    {
        public Raça RaçaMob { get; set; }
        public string Nome { get; set; }
        public float PontosDeVida { get; set; }
        public float PontosDeVidaMaxima { get; set; }
        public float AtaqueFisico { get; set; }
        public float DefesaFisica { get; set; }
        public float AtaqueMagico { get; set; }
        public float DefesaMagica { get; set; }
        public int Velocidade { get; set; }
        // public List<Hit> Hits { get; set; } // Aleatoriamente vai sair um dano que ele tiver e com base no estilo para mostrar ao jogador a fraqueza
        [BsonIgnore] public int ChanceDeAparecer { get; set; }
        // public List<ItemChanceCair> ChanceCairItem { get; set; } = new List<ItemChanceCair>();

        public Mob(string nome)
        {
            Nome = nome;
        }
    }

    //public class Hit
    //{
    //    public int Hits { get; set; }
    //    public string Nome { get; set; }
    //}
}
