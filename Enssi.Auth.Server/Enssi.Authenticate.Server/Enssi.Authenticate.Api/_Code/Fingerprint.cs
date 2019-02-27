using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using WebSocketSharp;

namespace Enssi.Authenticate.Api
{
    public class Fingerprint : IDisposable
    {
        WebSocket FingerprintWebSocket { get; set; }

        System.Timers.Timer HeartbeatTimer { get; set; } = new System.Timers.Timer(5000);

        public event Action<Guid?> OnCaptureResult;
        private Fingerprint()
        {
            HeartbeatTimer.Elapsed += HeartbeatTimer_Elapsed;
            HeartbeatTimer.Start();
        }

        public static Fingerprint CreateFingerprint()
        {
            var fingerprintWebSocket = new WebSocket("ws://127.0.0.1:" + ConfigurationManager.AppSettings["FingerprintPort"]);
            var fingerprint = new Fingerprint { FingerprintWebSocket = fingerprintWebSocket };
            fingerprintWebSocket.OnMessage += fingerprint.FingerprintWebSocket_OnMessage;
            fingerprintWebSocket.Connect();

            if (!fingerprintWebSocket.Ping("Ping"))
            {
                //XtraMessageBox.Show("指纹服务未开启！指纹设备不会生效！", "恩熙医疗His系统");
                //return null;
            }

            fingerprint.InitEngine();
            return fingerprint;
        }

        private void HeartbeatTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            FingerprintWebSocket.Send(JsonConvert.SerializeObject(new { method = "Heartbeat" }));
        }

        ~Fingerprint()
        {
            Dispose();
        }

        public void Dispose()
        {
            HeartbeatTimer.Stop();
            FingerprintWebSocket.Close();
        }

        public void InitEngine()
        {
            FingerprintWebSocket.Send(JsonConvert.SerializeObject(new { method = "InitEngine" }));
        }

        public void Capture(List<FingerprintCaptureItem> captureList, string template)
        {
            FingerprintWebSocket.Send(JsonConvert.SerializeObject(new { method = "Capture", CaptureList = captureList, Template = template }));
        }

        private void FingerprintWebSocket_OnMessage(object sender, MessageEventArgs e)
        {
            var message = JsonConvert.DeserializeAnonymousType(e.Data, new { method = default(string) });

            switch (message.method)
            {
                case "OnInitEngine":
                    var onInitEngineMessage = JsonConvert.DeserializeAnonymousType(e.Data, new { ErrorCode = default(int), Message = default(string), SensorSN = default(string) });
                    if (onInitEngineMessage.ErrorCode != 0)
                    {
                        //XtraMessageBox.Show("指纹设备初始化失败，指纹设备将不会生效。", "提示");
                    }
                    break;
                case "OnCaptureResult":
                    var onCaptureResultMessage = JsonConvert.DeserializeAnonymousType(e.Data, new { UserId = default(Guid?) });
                    OnCaptureResult?.Invoke(onCaptureResultMessage.UserId);
                    break;
                default:
                    break;
            }
        }
    }

    public class FingerprintCaptureItem
    {
        public Guid Id { get; set; }
        public string Data { get; set; }
    }
}
