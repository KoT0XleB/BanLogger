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
        public override Version Version => new Version(1, 0, 0);
        public override int Priority => int.MinValue;

        public override void Enable()
        {
            Qurre.Events.Player.Ban += OnBan;
            Qurre.Events.Player.Kick += OnKick;
        }

        public override void Disable()
        {
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
            string token = "https://discord.com/api/webhooks/id/token";
            string url = "https://data.whicdn.com/images/328582912/original.jpg";

            string details = String.Empty;
            details += "Пользователь, который был забанен: ";
            details += $"```{banEvent.Issuer.Nickname} ```";
            details += "Админ, который забанил: ";
            details += $"```{banEvent.Target.Nickname} ```";
            details += "По причине: \n";
            details += $"```{banEvent.Reason} ```";
            details += "Длительность бана: ";

            if (banEvent.Duration >= 31536000) details += $"```{(banEvent.Duration / 31536000).ToString()} year```";
            else if (banEvent.Duration >= 259200) details += $"```{(banEvent.Duration / 259200).ToString()} day```";
            else if (banEvent.Duration >= 3600) details += $"```{(banEvent.Duration / 3600).ToString()} hour```";
            else if (banEvent.Duration < 3600 && banEvent.Duration >= 60) details += $"```{(banEvent.Duration / 60).ToString()} min```";
            else details += $"```{(banEvent.Duration).ToString()} sec```";

            Log.Info(details);
            WebhookMessage($"{details}", "Логгер Баннов: BAN", "16711680", token, url);
        }

        public static void OnKick(KickEvent kickEvent)
        {
            string token = "https://discord.com/api/webhooks/923257729945055342/VZ-6W_wOdzuBugqoFpuQic__NUYaMVdAHEv_uhKVu7vS94pA5iHjXDTKiI2wKWFX5uDS";
            string url = "https://data.whicdn.com/images/328582912/original.jpg";

            string details = String.Empty;
            details += "Пользователь, который был забанен: ";
            details += $"```{kickEvent.Issuer.Nickname} ```";
            details += "Админ, который забанил: ";
            details += $"```{kickEvent.Target.Nickname} ```";
            details += "По причине: \n";
            details += $"```{kickEvent.Reason} ```";
            details += "Длительность бана: ";
            details += $"```Kick ```";
            Log.Info(details);

            //SendWebhook("Kick", kickEvent.Issuer.Nickname, kickEvent.Reason, kickEvent.Target.Nickname, string.Empty);
            WebhookMessage($"{details}", "Логгер Киков: KICK", "16711680", token, url);
        }
    }
}
