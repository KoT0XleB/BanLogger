using Qurre.API.Addons;
using System.Collections.Generic;
using System.ComponentModel;

namespace BanLogger
{
    public class Config : IConfig
    {
        [Description("Plugin Name")]
        public string Name { get; set; } = "BanLogger";

        [Description("Enable the plugin?")]
        public bool IsEnable { get; set; } = true;
        [Description("Logger Kick Name")]
        public string LoggerKickName { get; set; } = "Логгер Киков";
        [Description("Logger Ban Name")]
        public string LoggerBanName { get; set; } = "Логгер Баннов";
        [Description("The admin who banned")]
        public string Staff { get; set; } = "Админ, который забанил";
        [Description("The user who was banned")]
        public string UserBanned { get; set; } = "Пользователь, который был забанен";
        [Description("For a reason")]
        public string Reason { get; set; } = "По причине";
        [Description("Duration of the ban")]
        public string TimeBan { get; set; } = "Длительность бана";
        [Description("Kick string")]
        public string Kick { get; set; } = "Кик";
        [Description("Ban string")]
        public string Ban { get; set; } = "Бан";
        [Description("Color string")]
        public string Color { get; set; } = "16711680";
        [Description("Token from the webhook in the discord: https://discord.com/api/webhooks/webhook.id/webhook.token")]
        public string Token { get; set; } = string.Empty;
        [Description("URL photos for beauty. Located under the main information of the ban.")]
        public string UrlPhoto { get; set; } = "NotWorking!!!";
    }
}
