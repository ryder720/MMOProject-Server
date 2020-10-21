using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;



class ServerHandle
{
    public static void WelcomeRecieved(int _fromClient, Packet _packet)
    {
        int _clientIDCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected and is now player {_fromClient}");
        if (_fromClient != _clientIDCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the client ID {_clientIDCheck}");
        }
        Server.clients[_fromClient].SendIntoGame(_username);
    }

    public static void PlayerMovement(int _fromClient, Packet _packet)
    {
        bool[] _inputs = new bool[_packet.ReadInt()];
        for (int i = 0; i < _inputs.Length; i++)
        {
            _inputs[i] = _packet.ReadBool();
        }
        Quaternion _rotation = _packet.ReadQuaternion();

        Server.clients[_fromClient].player.SetInput(_inputs, _rotation);
    }

    public static void ChatMessage(int _fromClient, Packet _packet)
    {
        int _clientID = _packet.ReadInt();
        string _msg = _packet.ReadString();

        if (_fromClient != _clientID)
        {
            Debug.Log($"{_fromClient} has sent the wrong id: {_clientID}. Real sus");
        }

        ServerSend.SendChatMessage(_clientID, _msg); // Just for testing
    }
}
