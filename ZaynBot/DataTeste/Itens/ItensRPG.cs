using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.Data.Itens
{
    public class ItensRPG
    {
        public static RPGItem MoedaDeZeoinItem()
            => new RPGItem(0, "Moeda de Zeoin", TipoItemEnum.Moeda)
            {
                Descricao = "Uma moeda de Zeoin, vale 1 sense.",
            };


        public static RPGItem EspadaEnferrujadaItem()
            => new RPGItem(1, "Espada enferrujada de Bronze", TipoItemEnum.Arma, 100, 20, TipoExpEnum.Perfurante)
            {
                Descricao = "Uma espada de bronze muito desgastada devido ao uso constante.",
                AtaqueFisico = 20,
            };

        public static RPGItem OssosItem()
            => new RPGItem(2, "Osso", TipoItemEnum.Recurso, 1)
            {
                Descricao = "Ossos.. isso dá um arrepio."
            };


        public static RPGItem GosmaItem()
            => new RPGItem(3, "Gosma de Slime", TipoItemEnum.Recurso, 3)
            {
                Descricao = "Muito gosmento, onde gruda não sai com facilidade."
            };

        public static RPGItem CarneDeCoelhoAssadaItem()
            => new RPGItem(4, "Carne de Coelho assada", TipoItemEnum.Usavel, 4)
            {
                Descricao = "Uma carne bem cheirosa!",
                FomeRestaura = 2,
                VidaRestaura = 5,
            };

        public static RPGItem CarneDeCoelhoItem()
            => new RPGItem(5, "Carne de Coelho cru", TipoItemEnum.Recurso, 2)
            {
                Descricao = "Ainda fresco."
            };

        public static RPGItem OssoAfiadoItem()
            => new RPGItem(6, "Osso afiado", TipoItemEnum.Arma, 50, 0, TipoExpEnum.Perfurante)
            {
                Descricao = "Meio nojento, mas é bem afiado para uma arma.",
                AtaqueFisico = 10,
            };

        public static RPGItem FrascoVermelhoItem()
            => new RPGItem(7, "Frasco vermelho", TipoItemEnum.Usavel, 2)
            {
                Descricao = "Um frasco com um liquido vermelho..",
                VidaRestaura = 25
            };
    }
}
