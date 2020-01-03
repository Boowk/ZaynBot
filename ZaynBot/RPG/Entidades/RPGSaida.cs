namespace ZaynBot.RPG.Entidades
{
    public enum EnumDirecao
    {
        Norte = 0,
        Sul = 1,
        Oeste = 2,
        Leste = 3,
    }

    public class RPGSaida
    {
        public EnumDirecao Direcao { get; set; }
        public int RegiaoId { get; set; }

        public RPGSaida(EnumDirecao direcao, int regiaoId)
        {
            Direcao = direcao;
            RegiaoId = regiaoId;
        }
    }
}
