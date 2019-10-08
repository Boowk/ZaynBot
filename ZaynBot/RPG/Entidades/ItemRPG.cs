﻿using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class ItemRPG
    {
        [BsonId]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoItemEnum TipoItem { get; set; }
        public TipoExpEnum TipoExp { get; set; }
        public int Durabilidade { get; set; }
        public double PrecoBase { get; set; }

        public double AtaqueFisico { get; set; }
        public double DefesaFisica { get; set; }

        public double VidaRestaura { get; set; }
        public double MagiaRestaura { get; set; }
        public double FomeRestaura { get; set; }

        public ItemRPG(int id, string nome, TipoItemEnum tipo, double preco = 1)
        {
            Id = id;
            Nome = nome;
            TipoItem = tipo;
            PrecoBase = preco;
            Durabilidade = 0;
            TipoExp = TipoExpEnum.Nenhum;
        }

        public ItemRPG(int id, string nome, TipoItemEnum tipo, int durabilidade, double preco, TipoExpEnum tipoExp) : this(id, nome, tipo, preco)
        {
            TipoExp = tipoExp;
            Durabilidade = durabilidade;
        }
    }

    [BsonIgnoreExtraElements]
    public class ItemDataRPG
    {
        public int Id { get; set; }
        public int Durabilidade { get; set; }
        public int Quantidade { get; set; }
    }
}
