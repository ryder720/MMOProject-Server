using System;
using System.Collections.Generic;
using System.Text;


class ServerSend
{
    private static void SendTCPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].tcp.SendData(_packet);
    }

    private static void SendUDPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].udp.SendData(_packet);
    }

    private static void SendTCPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].tcp.SendData(_packet);
        }
    }

    private static void SendUDPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].udp.SendData(_packet);
        }
    }

    private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
                Server.clients[i].tcp.SendData(_packet);
        }
    }

    private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
                Server.clients[i].udp.SendData(_packet);
        }
    }

    #region Packets
    public static void Welcome(int _toClient, string _msg)
    {
        using (Packet _packet = new Packet((int)ServerPackets.welcome))
        {
            _packet.Write(_msg);
            _packet.Write(_toClient);

            SendTCPData(_toClient, _packet);
        }
    }

    public static void SpawnPlayer(int _toClient, Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.username);
            _packet.Write(_player.transform.position);
            _packet.Write(_player.transform.rotation);

            SendTCPData(_toClient, _packet);
        }
    }

    public static void SpawnActor(Actors _actor)
    {
        using (Packet _packet = new Packet((int)ServerPackets.spawnActor))
        {
            _packet.Write(_actor.actorID);
            _packet.Write(_actor.actorName);
            _packet.Write(_actor.transform.position);
            _packet.Write(_actor.transform.rotation);

            SendTCPDataToAll(_packet);
        }
    }

    public static void PlayerPosition(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.transform.position);

            SendUDPDataToAll(_packet);
        }
    }

    public static void PlayerRotation(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.transform.rotation);

            SendUDPDataToAll(_player.id, _packet);
        }
    }

    public static void ActorPosition(Actors _actor)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
        {
            _packet.Write(_actor.actorID);
            _packet.Write(_actor.transform.position);

            SendUDPDataToAll(_packet);
        }
    }

    public static void ActorRotation(Actors _actor)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
        {
            _packet.Write(_actor.actorID);
            _packet.Write(_actor.transform.rotation);

            SendUDPDataToAll(_packet);
        }
    }

    public static void PlayerDisconnected(int _playerID)
    {
        using(Packet _packet = new Packet((int)ServerPackets.playerDisconnected))
        {
            _packet.Write(_playerID);

            SendTCPDataToAll(_packet);
        }
    }

    public static void SendChatMessage(int _playerID, string _msg)
    {
        using(Packet _packet = new Packet((int)ServerPackets.chatUpdate))
        {
            _packet.Write(_playerID);
            _packet.Write(_msg);

            SendTCPDataToAll(_packet);
        }
    }

    public static void SendTellMessage(int _fromPlayerID, int _toPlayerID, string _msg)
    {
        using(Packet _packet = new Packet((int)ServerPackets.chatTell))
        {
            _packet.Write(_fromPlayerID);
            _packet.Write(_msg);

            SendTCPData(_toPlayerID, _packet);
            if(_fromPlayerID != 0)
                SendTCPData(_fromPlayerID, _packet);
        }
    }

    #endregion
}

