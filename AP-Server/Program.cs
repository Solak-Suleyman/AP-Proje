// See https://aka.ms/new-console-template for more information
using AP_Server;

Console.WriteLine("Hello, World!");
var server = new Server("127.0.0.1", 5000);
server.Start();