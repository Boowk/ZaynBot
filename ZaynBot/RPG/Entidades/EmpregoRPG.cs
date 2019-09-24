using DSharpPlus.CommandsNext;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZaynBot.RPG.Entidades
{
    public class EmpregoRPG
    {
        //public string Nome { get; set; }
        //public string Descrição { get; set; }
        //public int Nivel { get; set; } = 1;
        //public int NivelMax { get; set; } = 1;
        //public float ExperienciaAtual { get; set; } = 0;
        //public float ExperienciaProximoNivel { get; set; } = 1500;
        //public bool IsTrocaComPadre { get; set; } = true;

        //[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        //public Dictionary<int, string> DisponibilizaEmprego { get; set; } = new Dictionary<int, string>();
        //// Nivel que será desbloqueado o emprego, Nome do emprego para pesquisar na lista

        //[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        //public Dictionary<int, int> DisponibilizaHabilidade { get; set; } = new Dictionary<int, int>();
        //// Nivel que será desbloqueado a habilidade, nome da habilidade para pesquisar na lista

        //public EmpregoRPG(string nome)
        //    => Nome = nome;

        //public async Task AdicionarExpAsync(float exp, PersonagemRPG personagem, CommandContext ctx)
        //{
        //    float expResultante = ExperienciaAtual + exp;
        //    if (expResultante >= ExperienciaProximoNivel)
        //    {
        //        do
        //        {
        //            float quantosPrecisaProxNivel = expResultante - ExperienciaProximoNivel;
        //            await EvoluirAsync(personagem, ctx);
        //            expResultante = quantosPrecisaProxNivel;
        //        } while (expResultante >= ExperienciaProximoNivel);
        //        ExperienciaAtual += expResultante;
        //        return;
        //    }
        //    ExperienciaAtual += exp;
        //    return;
        //}

        //private async Task EvoluirAsync(PersonagemRPG personagem, CommandContext ctx)
        //{
        //    EmpregoRPG empregoAtual = ModuloBanco.EmpregoGet(Nome);
        //    if (Nivel == empregoAtual.NivelMax)
        //    {
        //        ExperienciaAtual = 0;
        //    }
        //    else
        //    {
        //        //Nivel += 1;
        //        //ExperienciaAtual = 0;
        //        //ExperienciaProximoNivel *= 1.25f;
        //        //await ctx.RespondAsync($"*⟦ {ctx.Member.Mention} subiu de Nível ⟧*");
        //        //// Verificamos se existe alguma habilidade no novo nivel do emprego
        //        //bool temHabilidade = empregoAtual.DisponibilizaHabilidade.TryGetValue(Nivel, out int habilidadedisponivel);
        //        //// Se tiver
        //        //if (temHabilidade)
        //        //{
        //        //    // Adicionamos a habilidade
        //        //    personagem.AdicionarHabilidade(habilidadedisponivel);
        //        //    await ctx.RespondAsync($"⟦ Habilidade de ⌈{empregoAtual.Nome.PrimeiraLetraMaiuscula()}⌋: ‖{ModuloBanco.HabilidadeGet(habilidadedisponivel).Nome}‖ obtida ⟧");
        //        //}
        //        //// Verificamos se tem emprego novo tambem
        //        //bool temEmprego = empregoAtual.DisponibilizaEmprego.TryGetValue(Nivel, out string empregodisponivel);
        //        //if (temEmprego)
        //        //{
        //        //    EmpregoRPG empregoNovo = ModuloBanco.EmpregoGet(empregodisponivel);
        //        //    empregoNovo.DisponibilizaEmprego.Clear();
        //        //    empregoNovo.DisponibilizaHabilidade.Clear();
        //        //    personagem.AdicionarEmprego(empregoNovo);
        //        //    await ctx.RespondAsync($"⟦ Emprego: ⌈{empregoNovo.Nome.PrimeiraLetraMaiuscula()}⌋ está disponível ⟧");
        //    }
        //}
    }
}
