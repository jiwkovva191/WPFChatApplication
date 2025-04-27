using System;
using System.Net;
using System.Net.Sockets;
using ChatServer.Net.IO;
namespace ChatServer
{
    class Program
    {
        static List<Client> _users;
        static TcpListener _listener;
        static void Main(string[] args)
        {
            _users = new List<Client>();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
            _listener.Start();

            while (true)
            {
                var client = new Client(_listener.AcceptTcpClient());
                _users.Add(client);

                /*broadcast the connection to everyone on the server*/
                BroadcastConnection();
            }


        }
        static void BroadcastConnection()
        {
            /*for each connected user on the server, send a message to all users*/
            foreach(var user in _users)
            {
                foreach(var usr in _users)
                {
                    var broadcastPacket = new PacketBuilder();
                    broadcastPacket.WriteOpCode(1);
                    broadcastPacket.WriteString(usr.Username);
                    broadcastPacket.WriteString(usr.UID.ToString());
                    user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                }
            }
        }
    }
}
