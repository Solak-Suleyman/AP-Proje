
using Client;

public class Program
{
    public static void Main(string[] args)
    {
        var client = new TestClient("127.0.0.1", 5000);

        var createUserCommand = new Command
        {
            Type = CommandType.CreateUser,
            Data = new Dictionary<string, string> { { "username", "user1" } }
        };

        var createRoomCommand = new Command
        {
            Type = CommandType.CreateRoom,
            Data = new Dictionary<string, string> { { "roomName", "room1" } }
        };

        var joinRoomCommand = new Command
        {
            Type = CommandType.JoinRoom,
            Data = new Dictionary<string, string> { { "roomName", "room1" }, { "username", "user1" } }
        };

        var sendMessageCommand = new Command
        {
            Type = CommandType.SendMessage,
            Data = new Dictionary<string, string> { { "roomName", "room1" }, { "message", "Hello, room1!" } }
        };

        while (true)
        {
            client.SendMessage(createUserCommand);

            client.SendMessage(createRoomCommand);
            client.SendMessage(joinRoomCommand);


            client.SendMessage(sendMessageCommand);

            client.ReceiveMessages();
            string input = Console.ReadLine();
            if (input == "1")
            {
                break;
            }
        }
        //client.SendMessage(createRoomCommand);
        //client.SendMessage(joinRoomCommand);
        //client.SendMessage(sendMessageCommand);

        //client.ReceiveMessages();

    }
}