using ZaynBot.RPG.Entidades;

namespace ZaynBot.Data.Itens
{
    public class ItensRPG
    {
        public static RPGItem ItemMoedaDeZeoin()
            => new RPGItem(0, "Moeda de Zeoin", EnumTipo.Moeda)
            {
                Descricao = "Uma moeda de Zeoin, troque por itens.",
            };


        public static RPGItem ItemEspadaEnferrujada()
            => new RPGItem(1, "Espada enferrujada de Bronze", EnumTipo.Arma, EnumProficiencia.Perfurante)
            {
                Descricao = "Uma espada de bronze muito desgastada devido ao uso constante.",
                AtaqueFisico = 20,
            };

        public static RPGItem ItemOssos()
            => new RPGItem(2, "Ossos", EnumTipo.Recurso)
            {
                Descricao = "Ossos são para enterrar.",
                PrecoVenda = 2,
            };

        public static RPGItem ItemCarneDeCoelhoCru()
            => new RPGItem(3, "Carne de coelho cru", EnumTipo.Recurso)
            {
                Descricao = "Deve ter um gosto melhor cozido.",
                PrecoVenda = 4,
            };

        public static RPGItem ItemCouroDeVaca()
            => new RPGItem(4, "Couro de vaca", EnumTipo.Recurso)
            {
                Descricao = "Eu deveria levar isso para um curtume.",
                PrecoVenda = 6,
            };

        public static RPGItem ItemCarneDeVacaCru()
             => new RPGItem(5, "Carne de vaca cru", EnumTipo.Recurso)
             {
                 Descricao = "Deve ter um gosto melhor cozido.",
                 PrecoVenda = 4,
             };

        public static RPGItem ItemCarneDeGalinhaCru()
             => new RPGItem(6, "Carne de galinha cru", EnumTipo.Recurso)
             {
                 Descricao = "Deve ter um gosto melhor cozido.",
                 PrecoVenda = 5,
             };

        public static RPGItem ItemPenas()
            => new RPGItem(8, "Penas", EnumTipo.Recurso)
            {
                Descricao = "Usado para pesca com mosca.",
                PrecoVenda = 1,
            };

        public static RPGItem ItemFrascoVermelho()
            => new RPGItem(7, "Frasco vermelho", EnumTipo.Pocao)
            {
                Descricao = "Um frasco com um liquido vermelho.",
                PrecoCompra = 10,
                PrecoVenda = 4,
                VidaRestaura = 25,
            };
    }
}
