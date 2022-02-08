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
        public string LoggerKickName { get; set; } = "Logger Kick";
        [Description("Logger Ban Name")]
        public string LoggerBanName { get; set; } = "Logger Ban";
        [Description("The admin who banned")]
        public string Staff { get; set; } = "The admin who banned";
        [Description("The user who was banned")]
        public string UserBanned { get; set; } = "The user who was banned";
        [Description("For a reason")]
        public string Reason { get; set; } = "For a reason";
        [Description("Duration of the ban")]
        public string TimeBan { get; set; } = "Duration of the ban";
        [Description("Kick string")]
        public string Kick { get; set; } = "Kick";
        [Description("Ban string")]
        public string Ban { get; set; } = "Ban";
        [Description("Webhook username")]
        public string UserNameBot { get; set; } = "BanLogger";
        [Description("Webhook avatar-url")]
        public string AvatarUrlBot { get; set; } = "https://cdn.discordapp.com/attachments/795751309965131788/940717183871373402/qurre_ol.png";
        [Description("Webhook text")]
        public string ContentBot { get; set; } = string.Empty;
        [Description("Webhook color decimal")]
        public string Color { get; set; } = "65280";
        [Description("Webhook image")]
        public string ImageBot { get; set; } = "https://cdn.discordapp.com/attachments/897081711773495306/940726310844629002/download.png";
        [Description("Webhook author name")]
        public string AutorName { get; set; } = "№1 Server Qurre";
        [Description("Webhook author icon image")]
        public string AutorIconUrl { get; set; } = "https://cdn.discordapp.com/attachments/795751309965131788/940717183871373402/qurre_ol.png";
        [Description("Webhook thumbnail image")]
        public string ThumbnailUrlBot { get; set; } = string.Empty;
        [Description("Webhook footer text")]
        public string FooterTextBot { get; set; } = string.Empty;

        [Description("Url from the webhook in the discord: https://discord.com/api/webhooks/webhook.id/webhook.token")]
        public string Url { get; set; } = string.Empty;
    }
}
