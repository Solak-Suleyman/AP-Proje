using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public enum CommandType
    {
        CreateUser,
        CreateRoom,
        JoinRoom,
        SendMessage
    }

    public class Command
    {
        [JsonProperty("type")]
        public CommandType Type { get; set; }
        [JsonProperty("data")]
        public Dictionary<string, string> Data { get; set; }
    }
    public class TestClient
    {
        private TcpClient _client;
        private NetworkStream _stream;

        public TestClient(string ipAddress, int port)
        {
            _client = new TcpClient(ipAddress, port);
            _stream = _client.GetStream();
        }



        public void SendMessage(Command command)
        {
            var message = JsonConvert.SerializeObject(command);
            message += "\n";
            var buffer = Encoding.UTF8.GetBytes(message);
            _stream.Write(buffer, 0, buffer.Length);
        }

        public void ReceiveMessages()
        {
            var buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = _stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {message}");
            }
        }
    }


}
