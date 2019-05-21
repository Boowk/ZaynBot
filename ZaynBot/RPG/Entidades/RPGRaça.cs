using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGRaça
    {
        public string Nome { get; private set; }
        [BsonIgnore] public float PontosDeVidaBaseMin { get; private set; }
        [BsonIgnore] public float PontosDeManaBaseMin { get; private set; }
        [BsonIgnore] public float AtaqueFisicoBaseMin { get; private set; }
        [BsonIgnore] public float DefesaFisicaBaseMin { get; private set; }
        [BsonIgnore] public float AtaqueMagicoBaseMin { get; private set; }
        [BsonIgnore] public float DefesaMagicaBaseMin { get; private set; }
        [BsonIgnore] public int VelocidadeBaseMin { get; private set; }
        [BsonIgnore] public int SorteBaseMin { get; private set; }

        [BsonIgnore] public float PontosDeVidaBaseMax { get; private set; }
        [BsonIgnore] public float PontosDeManaBaseMax { get; private set; }
        [BsonIgnore] public float AtaqueFisicoBaseMax { get; private set; }
        [BsonIgnore] public float DefesaFisicaBaseMax { get; private set; }
        [BsonIgnore] public float AtaqueMagicoBaseMax { get; private set; }
        [BsonIgnore] public float DefesaMagicaBaseMax { get; private set; }
        [BsonIgnore] public int VelocidadeBaseMax { get; private set; }
        [BsonIgnore] public int SorteBaseMax { get; private set; }

        public RPGRaça(string nome)
        {
            Nome = nome;
        }

        public void SetPontosDeVidaBase(float min, float max)
        {
            PontosDeVidaBaseMin = min;
            PontosDeVidaBaseMax = max;
        }
        public void SetPontosDeManaBase(float min, float max)
        {
            PontosDeManaBaseMin = min;
            PontosDeManaBaseMax = max;
        }
        public void SetAtaqueFisicoBase(float min, float max)
        {
            AtaqueFisicoBaseMin = min;
            AtaqueFisicoBaseMax = max;
        }
        public void SetDefesaFisicaBase(float min, float max)
        {
            DefesaFisicaBaseMin = min;
            DefesaFisicaBaseMax = max;
        }
        public void SetAtaqueMagicoBase(float min, float max)
        {
            AtaqueMagicoBaseMin = min;
            AtaqueMagicoBaseMax = max;
        }
        public void SetDefesaMagicaBase(float min, float max)
        {
            DefesaMagicaBaseMin = min;
            DefesaMagicaBaseMax = max;
        }
        public void SetVelocidadeBase(int min, int max)
        {
            VelocidadeBaseMin = min;
            VelocidadeBaseMax = max;
        }
        public void SetSorteBase(int min, int max)
        {
            SorteBaseMin = min;
            SorteBaseMax = max;
        }
    }
}
