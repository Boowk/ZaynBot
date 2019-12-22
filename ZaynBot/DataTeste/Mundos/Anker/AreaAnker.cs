using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Data.Mundos.Anker
{
    public static class AreaAnker
    {
        public static RPGRegiao LugarDesconhecido0()
        {
            RPGRegiao regiao = new RPGRegiao
            {
                Id = 0,
                Nome = "Lugar desconhecido",
                Descrição = "Sinto uma familiaridade com este lugar, mas não consigo lembrar o porque... " +
                "Uma sala mofada, úmida e pequena, com somente um candelabro no canto da parede para iluminar toda a sala. " +
                "Ao olhar a faca no canto, acabo me tremendo de arrepio, como se algo ruim acabou de acontecer. " +
                "Vejo uma pequena luz a *Leste*.",
                Dificuldade = 1
            };
            RPGSaida leste = new RPGSaida(DirecaoEnum.Leste, 1);
            regiao.SaidasRegioes.Add(leste);

            regiao.LojaItensId.Add(4);
            regiao.LojaItensId.Add(1);
            return regiao;
        }
        public static RPGRegiao LugarDesconhecido1()
        {

            RPGRegiao regiao = new RPGRegiao
            {
                Id = 1,
                Nome = "Lugar desconhecido",
                Descrição = "Após chegar aqui, o caminho atrás se fechou, ficando somente uma parede no lugar. " +
                "A *Leste* vejo uma porta com luzes saindo pelas frestas da porta. " +
                "Graças a luz consigo também ver um caminho sombrio e escuro a *Norte*, " +
                "sempre que olho para este caminho sinto um frio penetrar a minha espinha, " +
                "como se tivesse algo me observando.. O mofo aqui é menor, graças a um vento que " +
                "passa pela porta limpando todo o ar dentro desta sala. ",
            };
            RPGSaida leste = new RPGSaida(DirecaoEnum.Leste, 2);
            RPGSaida norte = new RPGSaida(DirecaoEnum.Norte, 3);
            regiao.SaidasRegioes.Add(leste);
            regiao.SaidasRegioes.Add(norte);
            return regiao;
        }
        public static RPGRegiao LugarDesconhecido2()
        {
            RPGRegiao regiao = new RPGRegiao
            {
                Id = 2,
                Nome = "Lugar desconhecido",
                Descrição = "Agora que cheguei aqui, percebo que onde eu sempre estive era em uma caverna, " +
                "está sala está muito bem iluminada, com candelabros pendurados na parede todos cheios de " +
                "velas longas e acesas. Mas o que chama mais atenção é o lustre de bolas negras no teto " +
                "emitindo um frio que acaba iluminando toda a sala de azul. O que torna esta sala mais estranha " +
                "é este chão grudento... Exergo uma porta a *Sul*.",
            };
            RPGSaida sul = new RPGSaida(DirecaoEnum.Sul, 4);
            RPGSaida oeste = new RPGSaida(DirecaoEnum.Oeste, 1);
            regiao.SaidasRegioes.Add(sul);
            regiao.SaidasRegioes.Add(oeste);
            return regiao;
        }
        public static RPGRegiao LugarDesconhecido3()
        {
            RPGRegiao regiao = new RPGRegiao
            {
                Id = 3,
                Nome = "Lugar desconhecido",
                Descrição = "Não consigo enxergar direito neste lugar escuro, mas sempre que dou um passo meu " +
                "pé afunda em algo frágil, como se fosse galhos.. Agora que peguei um destes 'galhos' " +
                "para examinar melhor, vejo que é OSSO! Estou em um mar de ossos! Estes lugar " +
                "me deixam com muito medo... *Algo me observa de longe*.",
            };
            RPGSaida sul = new RPGSaida(DirecaoEnum.Sul, 1);
            regiao.SaidasRegioes.Add(sul);
            return regiao;
        }
        public static RPGRegiao SaidaDaCaverna4()
        {
            RPGRegiao regiao = new RPGRegiao
            {
                Id = 4,
                Nome = "Saída da Caverna",
                Descrição = "Finalmente um pouco de ar! Está caverna está bem escondida nesta floresta, " +
                "se eu passa se por aqui nunca teria visto a entrada... Não quero nem pensar como eu vim " +
                "parar aqui. Acredito que com o tempo minha memória voltara. Até lá acho que devo achar algo " +
                "para comer nesta floresta.",
            };
            RPGSaida norte = new RPGSaida(DirecaoEnum.Norte, 2);
            regiao.SaidasRegioes.Add(norte);
            return regiao;
        }
    }
}
