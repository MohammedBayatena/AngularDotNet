using System.Text.Json.Serialization;

namespace dotnet_rpg.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight,
        Wizard,
        Healer
    }
}