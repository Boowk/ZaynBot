using ZaynBot.DataTeste.Mobs.Normal;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Data.Mundos.Anker
{
    public static class AreaAnker
    {
        public static RegiaoRPG LugarDesconhecido0()
        {
            RegiaoRPG regiao = new RegiaoRPG
            {
                Id = 0,
                Nome = "Lugar desconhecido",
                Descrição = "Sinto uma familiaridade com este lugar, mas não consigo lembrar o porque... " +
                "Uma sala mofada, úmida e pequena, com somente um candelabro no canto da parede para iluminar toda a sala. " +
                "Ao olhar a faca no canto, acabo me tremendo de arrepio, como se algo ruim acabou de acontecer. " +
                "Vejo uma pequena luz a *Leste*.",
            };
            SaidaRPG leste = new SaidaRPG(DirecaoEnum.Leste, 1);
            regiao.SaidasRegioes.Add(leste);
            return regiao;
        }
        public static RegiaoRPG LugarDesconhecido1()
        {

            RegiaoRPG regiao = new RegiaoRPG
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
            SaidaRPG leste = new SaidaRPG(DirecaoEnum.Leste, 2);
            SaidaRPG norte = new SaidaRPG(DirecaoEnum.Norte, 3);
            regiao.SaidasRegioes.Add(leste);
            regiao.SaidasRegioes.Add(norte);
            return regiao;
        }
        public static RegiaoRPG LugarDesconhecido2()
        {
            RegiaoRPG regiao = new RegiaoRPG
            {
                Id = 2,
                Nome = "Lugar desconhecido",
                Descrição = "Agora que cheguei aqui, percebo que onde eu sempre estive era em uma caverna, " +
                "está sala está muito bem iluminada, com candelabros pendurados na parede todos cheios de " +
                "velas longas e acesas. Mas o que chama mais atenção é o lustre de bolas negras no teto " +
                "emitindo um frio que acaba iluminando toda a sala de azul. O que torna esta sala mais estranha " +
                "é este chão grudento... Exergo uma porta a *Sul*.",
            };
            SaidaRPG sul = new SaidaRPG(DirecaoEnum.Sul, 4);
            SaidaRPG oeste = new SaidaRPG(DirecaoEnum.Oeste, 1);
            regiao.SaidasRegioes.Add(sul);
            regiao.SaidasRegioes.Add(oeste);
            regiao.Mobs.Add(MobsAnker.SlimeMobAb());
            return regiao;
        }
        public static RegiaoRPG LugarDesconhecido3()
        {
            RegiaoRPG regiao = new RegiaoRPG
            {
                Id = 3,
                Nome = "Lugar desconhecido",
                Descrição = "Não consigo enxergar direito neste lugar escuro, mas sempre que dou um passo meu " +
                "pé afunda em algo frágil, como se fosse galhos.. Agora que peguei um destes 'galhos' " +
                "para examinar melhor, vejo que é OSSO! Estou em um mar de ossos! Estes lugar " +
                "me deixam com muito medo... *Algo me observa de longe*.",
            };
            SaidaRPG sul = new SaidaRPG(DirecaoEnum.Sul, 1);
            regiao.SaidasRegioes.Add(sul);
            regiao.Mobs.Add(MobsAnker.EsqueletoMobAb());
            return regiao;
        }
        public static RegiaoRPG SaidaDaCaverna4()
        {
            RegiaoRPG regiao = new RegiaoRPG
            {
                Id = 4,
                Nome = "Saída da Caverna",
                Descrição = "Finalmente um pouco de ar! Está caverna está bem escondida nesta floresta, " +
                "se eu passa se por aqui nunca teria visto a entrada... Não quero nem pensar como eu vim " +
                "parar aqui. Acredito que com o tempo minha memória voltara. Até lá acho que devo achar algo " +
                "para comer nesta floresta.",
            };
            SaidaRPG norte = new SaidaRPG(DirecaoEnum.Norte, 2);
            regiao.SaidasRegioes.Add(norte);
            regiao.Mobs.Add(MobsAnker.CoelhoMobAb());
            return regiao;
        }
    }
}
