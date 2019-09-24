using System;

namespace ZaynBot.RPG.Entidades.Enuns
{
    [Flags]
    public enum MensagemAvisoEnum
    {
        PersonagemNull = 1 << 0,
        SemVida = 1 << 1,
        Todos = PersonagemNull | SemVida
    }
}
