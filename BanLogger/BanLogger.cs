using System;
using Qurre;
using Qurre.API.Events;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace BanLogger
{
    public class BanLogger : Plugin
    {
        public override string Developer => "KoToXleB#4663";
        public override string Name => "BanLogger";
        public override Version Version => new Version(2, 0, 0);
        public override int Priority => int.MinValue;
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
        public static void WebhookMessage(string description, string title, string color, string token, string url)
        {
            WebRequest web = WebRequest.CreateHttp(token);
            web.ContentType = "application/json";
            web.Method = "POST";
            using (var sw = new StreamWriter(web.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new
                {
                    username = "BanLogger",
                    embeds = new[]
                    {
                        new
                        {
                            description,
                            title,
                            color
                        }
                    }
                });
                sw.Write(json);
            }
            var response = web.GetResponse();
        }
        public static void OnBan(BanEvent banEvent)
        {
            if (CustomConfig.Token != string.Empty)
            {
                string details = String.Empty;
                details += $"{CustomConfig.Staff}: ";
                details += $"```{banEvent.Issuer.Nickname} ```";
                details += $"{CustomConfig.UserBanned}: ";
                details += $"```{banEvent.Target.Nickname} ```";
                details += $"{CustomConfig.Reason}: \n";
                details += $"```{banEvent.Reason} ```";
                details += $"{CustomConfig.TimeBan}: ";

                if (banEvent.Duration >= 31536000) details += $"```{(banEvent.Duration / 31536000).ToString()} year```";
                else if (banEvent.Duration >= 259200) details += $"```{(banEvent.Duration / 259200).ToString()} day```";
                else if (banEvent.Duration >= 3600) details += $"```{(banEvent.Duration / 3600).ToString()} hour```";
                else if (banEvent.Duration < 3600 && banEvent.Duration >= 60) details += $"```{(banEvent.Duration / 60).ToString()} min```";
                else details += $"```{(banEvent.Duration).ToString()} sec```";

                Log.Info(details);
                WebhookMessage($"{details}", $"{CustomConfig.LoggerBanName}: {CustomConfig.Ban}", $"{CustomConfig.Color}", $"{CustomConfig.Token}", $"{CustomConfig.UrlPhoto}");
            }
            else Log.Info("Error BanLogger");
        }

        public static void OnKick(KickEvent kickEvent)
        {
            if (CustomConfig.Token != string.Empty)
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
                Log.Info(details);

                WebhookMessage($"{details}", $"{CustomConfig.LoggerKickName}: {CustomConfig.Kick}", $"{CustomConfig.Color}", $"{CustomConfig.Token}", $"{CustomConfig.UrlPhoto}");
            }
            else Log.Info("Error KickLogger");
        }
    }
}
