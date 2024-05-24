using AP_Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class Server
{
    private TcpListener _listener;
    private List<User> _users = new List<User>();
    private List<Room> _rooms = new List<Room>();

    public Server(string ipAddress, int port)
    {
        _listener = new TcpListener(IPAddress.Parse(ipAddress), port);
    }

    public void Start()
    {
        _listener.Start();
        Console.WriteLine("Server started...");

        while (true)
        {
            var client = _listener.AcceptTcpClient();
            Task.Run(() => HandleClient(client));
        }
    }

    private void HandleClient(TcpClient client)
    {
        var stream = client.GetStream();
        var buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
        {
            var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received: {message}");

            // Process the message (create user, create/join room, send message to room)
            message = message.Trim();
            var messages = message.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var msg in messages)
            {
                ProcessMessage(client, msg);
            }

        }
    }

    private void ProcessMessage(TcpClient client, string message)
    {
        // Deserialize message and handle different commands
        Command command = JsonConvert.DeserializeObject<Command>(message);

        switch (command.Type)
        {
            case CommandType.CreateUser:
                CreateUser(client, command);
                break;
            case CommandType.CreateRoom:
                CreateRoom(client, command);
                break;
            case CommandType.JoinRoom:
                JoinRoom(client, command);
                break;
            case CommandType.SendMessage:
                SendMessageToRoom(client, command);
                break;
            default:
                Console.WriteLine("Unknown command");
                break;
        }
    }

    private void CreateUser(TcpClient client, Command command)
    {
        var username = command.Data["username"];
        var user = new User { UserName = username, Client = client };
        _users.Add(user);
        Console.WriteLine($"User created: {username}");
    }

    private void CreateRoom(TcpClient client, Command command)
    {
        var roomName = command.Data["roomName"];
        var room = new Room { Name = roomName };
        _rooms.Add(room);
        Console.WriteLine($"Room created: {roomName}");
    }

    private void JoinRoom(TcpClient client, Command command)
    {
        var roomName = command.Data["roomName"];
        var username = command.Data["username"];
        var room = _rooms.FirstOrDefault(r => r.Name == roomName);
        var user = _users.FirstOrDefault(u => u.UserName == username);

        if (room != null && user != null)
        {
            room.Users.Add(user);

            Console.WriteLine($"{username} joined room: {roomName}");
        }
    }

    private void SendMessageToRoom(TcpClient client, Command command)
    {
        var roomName = command.Data["roomName"];
        var message = command.Data["message"];
        var room = _rooms.FirstOrDefault(r => r.Name == roomName);

        if (room != null)
        {
            foreach (var user in room.Users)
            {
                var stream = user.Client.GetStream();
                var buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
            }
            Console.WriteLine($"Message sent to room {roomName}: {message}");
        }
    }
}

public enum CommandType
{
    CreateUser,
    CreateRoom,
    JoinRoom,
    SendMessage,
    Shoot,
    Move,
}

public class Command
{
    [JsonProperty("type")]
    public CommandType Type { get; set; }
    [JsonProperty("data")]
    public Dictionary<string, string> Data { get; set; }
}
