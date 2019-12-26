using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.Data.Itens
{
    public class ItensRPG
    {
        public static RPGItem ItemMoedaDeZeoin()
            => new RPGItem(0, "Moeda de Zeoin", TipoItemEnum.Moeda)
            {
                Descricao = "Uma moeda de Zeoin, troque por itens.",
            };


        public static RPGItem ItemEspadaEnferrujada()
            => new RPGItem(1, "Espada enferrujada de Bronze", TipoItemEnum.Arma, 100, TipoExpEnum.Perfurante)
            {
                Descricao = "Uma espada de bronze muito desgastada devido ao uso constante.",
                AtaqueFisico = 20,
            };

        public static RPGItem ItemOssos()
            => new RPGItem(2, "Ossos", TipoItemEnum.Recurso)
            {
                Descricao = "Ossos são para enterrar."
            };

        public static RPGItem ItemCarneDeCoelhoCru()
            => new RPGItem(3, "Carne de coelho cru", TipoItemEnum.Recurso)
            {
                Descricao = "Deve ter um gosto melhor cozido."
            };

        public static RPGItem ItemCouroDeVaca()
            => new RPGItem(4, "Couro de vaca", TipoItemEnum.Recurso)
            {
                Descricao = "Eu deveria levar isso para um curtume."
            };

        public static RPGItem ItemCarneDeVacaCru()
             => new RPGItem(5, "Carne de vaca cru", TipoItemEnum.Recurso)
             {
                 Descricao = "Deve ter um gosto melhor cozido."
             };

        public static RPGItem ItemCarneDeGalinhaCru()
             => new RPGItem(6, "Carne de galinha cru", TipoItemEnum.Recurso)
             {
                 Descricao = "Deve ter um gosto melhor cozido."
             };

        public static RPGItem ItemPenas()
            => new RPGItem(8, "Penas", TipoItemEnum.Recurso)
            {
                Descricao = "Usado para pesca com mosca."
            };

        public static RPGItem ItemFrascoVermelho()
            => new RPGItem(7, "Frasco vermelho", TipoItemEnum.Pocao)
            {
                Descricao = "Um frasco com um liquido vermelho.",
                VidaRestaura = 25,
            };
    }
}
