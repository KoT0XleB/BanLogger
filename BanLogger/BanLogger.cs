using System;
using Qurre;
using Qurre.API.Events;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Threading;

namespace BanLogger
{
    public class BanLogger : Plugin
    {
        public override string Developer => "KoT0XleB#4663";
        public override string Name => "BanLogger";
        public override Version Version => new Version(3, 1, 0);
        public override int Priority => 10;
        public override void Enable() => RegisterEvents();
        public override void Disable() => UnregisterEvents();
        public static Config CustomConfig { get; set; }

        public void RegisterEvents()
        {
            CustomConfig = new Config();
            CustomConfigs.Add(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Qurre.Events.Player.Ban += OnBan;
            Qurre.Events.Player.Kick += OnKick;
        }
        public void UnregisterEvents()
        {
            CustomConfigs.Remove(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Qurre.Events.Player.Ban -= OnBan;
            Qurre.Events.Player.Kick -= OnKick;
        }
        public static void WebhookMessage(string description, string title, string color, string url)
        {
            WebRequest web = WebRequest.CreateHttp(url);
            web.ContentType = "application/json";
            web.Method = "POST";
            using (var sw = new StreamWriter(web.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new
                {
                    username = CustomConfig.UserNameBot,
                    avatar_url = CustomConfig.AvatarUrlBot,
                    content = CustomConfig.ContentBot,
                    embeds = new[]
                    {
                        new
                        {
                            description = description,
                            title = title,
                            url = string.Empty,
                            color = color,
                            image = new 
                            {
                                url = CustomConfig.ImageBot
                            },
                            author = new
                            {
                                name = CustomConfig.AutorName,
                                icon_url = CustomConfig.AutorIconUrl
                            },
                            thumbnail = new
                            {
                                url = CustomConfig.ThumbnailUrlBot
                            },
                            footer = new
                            {
                                text = CustomConfig.FooterTextBot
                            }
                        }
                    }
                });
                sw.Write(json);
            }
            var response = web.GetResponse();
        }
        public static void OnBan(BanEvent banEvent)
        {
            if (CustomConfig.Url != string.Empty)
            {
                string details = String.Empty;
                details += $"{CustomConfig.Staff}: ";
                details += $"```{banEvent.Issuer.Nickname} ```";
                details += $"{CustomConfig.UserBanned}: ";
                details += $"```{banEvent.Target.Nickname} ```";
                details += $"{CustomConfig.Reason}: \n";
                details += $"```{banEvent.Reason} ```";
                details += $"{CustomConfig.TimeBan}: ";

                if (banEvent.Duration >= 31536000) details += $"```{(banEvent.Duration / 31536000)} year```";
                else if (banEvent.Duration >= 259200) details += $"```{(banEvent.Duration / 259200)} day```";
                else if (banEvent.Duration >= 3600) details += $"```{(banEvent.Duration / 3600)} hour```";
                else if (banEvent.Duration < 3600 && banEvent.Duration >= 60) details += $"```{(banEvent.Duration / 60)} min```";
                else details += $"```{(banEvent.Duration)} sec```";

                try
                {
                    Log.Info($"{banEvent.Issuer.Nickname} banned {banEvent.Target.Nickname} from the server.");
                    new Thread(() => { WebhookMessage($"{details}", $"{CustomConfig.LoggerBanName}: {CustomConfig.Ban}", $"{CustomConfig.Color}", $"{CustomConfig.Url}"); }).Start();
                }
                catch(Exception e) { Log.Error(e); }
            }
            else Log.Info("Error: URL is Empty.");
        }

        public static void OnKick(KickEvent kickEvent)
        {
            if (CustomConfig.Url != string.Empty)
            {
                string details = String.Empty;
                details += $"{CustomConfig.Staff}: ";
                details += $"```{kickEvent.Issuer.Nickname} ```";
                details += $"{CustomConfig.UserBanned}: ";
                details += $"```{kickEvent.Target.Nickname} ```";
                details += $"{CustomConfig.Reason}: \n";
                details += $"```{kickEvent.Reason} ```";
                details += $"{CustomConfig.TimeBan}: ";
                details += $"```{CustomConfig.Kick} ```";

                try
                {
                    Log.Info($"{kickEvent.Issuer.Nickname} kicked {kickEvent.Target.Nickname} from the server.");
                    new Thread(() => { WebhookMessage($"{details}", $"{CustomConfig.LoggerKickName}: {CustomConfig.Kick}", $"{CustomConfig.Color}", $"{CustomConfig.Url}"); }).Start();
                }
                catch (Exception e) { Log.Error(e); }
            }
            else Log.Info("Error: URL is Empty.");
        }
    }
}
