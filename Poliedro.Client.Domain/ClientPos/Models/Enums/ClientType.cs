using System.ComponentModel;

namespace Poliedro.Client.Domain.ClientPos.Models.Enums
{
    public enum ClientType
    {
        [Description("Default Client")]
        Default,
        [Description("Natural Client")]
        Natural,
        [Description("Legal Client")]
        Legal
    }
}
