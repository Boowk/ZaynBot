using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.RPG.Comandos
{
    class EquipamentosComando
    {
    }
}


//public void GerarEquips(StringBuilder sr, string nomeExibicao, TipoItemEnum itemEnum, PersonagemRPG personagem)
//{
//    sr.Append($"**{nomeExibicao}:** ");
//    bool isItem = personagem.Inventario.Equipamentos.TryGetValue(itemEnum, out ItemRPG item);
//    if (isItem)
//        sr.AppendLine($"{item.Nome}({item.Id}) - *Durab. {item.Durabilidade}/{ModuloBanco.ItemGet(item.Id).Durabilidade}*");
//    else
//        sr.AppendLine("Nenhum");
//}