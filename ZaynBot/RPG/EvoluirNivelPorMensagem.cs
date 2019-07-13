using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.Utilidades;

namespace ZaynBot.RPG
{
    public static class EvoluirNivelPorMensagem
    {
        public static async Task ReceberXPNivelMensagens(MessageCreateEventArgs e)
        {
            RPGUsuario usuario = ModuloBanco.GetUsuario(e.Author.Id);
            if (usuario != null)
            {
                if (DateTime.UtcNow >= usuario.DataUltimaMensagemEnviada)
                {
                    if (usuario.Personagem.PontosDeVida > 0)
                    {
                        usuario.DataUltimaMensagemEnviada = DateTime.UtcNow.AddMinutes(2).AddSeconds(30);
                        usuario.Personagem.Habilidades.TryGetValue("regeneração", out RPGHabilidade cura);
                        if (cura != null)
                        {
                            Sortear sort = new Sortear();

                            cura.AdicionarExp((float)sort.Valor(5, 25));
                            float quantidadeCura = usuario.Personagem.DefesaMagica * cura.CuraQuantidadePorcentagem;
                            usuario.Personagem.PontosDeVida += quantidadeCura;
                            if (usuario.Personagem.PontosDeVida > usuario.Personagem.PontosDeVidaMaxima)
                                usuario.Personagem.PontosDeVida = usuario.Personagem.PontosDeVidaMaxima;
                            float quantidadeMana = usuario.Personagem.PontosDeManaMaximo * cura.CuraQuantidadePorcentagem;
                            usuario.Personagem.PontosDeMana += quantidadeMana;
                            if (usuario.Personagem.PontosDeMana > usuario.Personagem.PontosDeManaMaximo)
                                usuario.Personagem.PontosDeMana = usuario.Personagem.PontosDeManaMaximo;
                            ModuloBanco.UpdateUsuario(usuario);
                        }
                    }
                }
            }
            await Task.CompletedTask;
        }
    }
}
