namespace ZaynBot.RPG.Entidades
{
    public class RPGMapa
    {
        private bool norte;
        private bool sul;
        private bool leste;
        private bool oeste;

        private string Norte
        {
            get
            {
                if (norte)
                    return Emojis.QuadradoNorte;
                else
                    return Emojis.QuadradoAzul;
            }
        }

        private string Sul
        {
            get
            {
                if (sul)
                    return Emojis.QuadradoSul;
                else
                    return Emojis.QuadradoAzul;
            }
        }

        private string Leste
        {
            get
            {
                if (leste)
                    return Emojis.QuadradoLeste;
                else
                    return Emojis.QuadradoAzul;
            }
        }

        private string Oeste
        {
            get
            {
                if (oeste)
                    return Emojis.QuadradoOeste;
                else
                    return Emojis.QuadradoAzul;
            }
        }


        public void AdicionarRegiao(RPGSaida saida)
        {
            switch (saida.Direcao)
            {
                case EnumDirecao.Norte:
                    norte = true;
                    break;
                case EnumDirecao.Sul:
                    sul = true;
                    break;
                case EnumDirecao.Leste:
                    leste = true;
                    break;
                case EnumDirecao.Oeste:
                    oeste = true;
                    break;
            }
        }

        public string Criar()
        {
            return $"{Emojis.QuadradoAzul}{Norte}{Emojis.QuadradoAzul}\n" +
                   $"{Oeste}{Emojis.Mago}{Leste}\n" +
                   $"{Emojis.QuadradoAzul}{Sul}{Emojis.QuadradoAzul}";
        }
    }
}
