
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    public GameObject playerPrefab;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Debug.Log("Instance already exists, destroying");
            Destroy(this);
        }
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        Server.Start(50,26883);


    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }
    public Player InstantiatePlayer()
    {
        return Instantiate(playerPrefab, new Vector3(1586.9f,35.5f,1763.9f), Quaternion.identity).GetComponent<Player>();
    }

    public void ExecuteCommand(int _id, string[] _command)
    {
        switch (_command[0])
        {
            case "/say":
                SayCommand(_id, _command);
                break;
            case "/cast":
                CastCommand(_id, _command);
                break;
            case "/tell":
                SendTellCommand(_id, _command);
                break;
            default:
                // No real command given

                break;

        }
    }

    #region Commands
    #region UniversalCommands
    public void SayCommand(int _id, string[] _command)
    {
        string _msg = "";
        StringBuilder _builder = new StringBuilder();  // Weird c# thing I learned today lol
        for (int i = 1; i < _command.Length; i++)
        {
            _builder.Append(_command[i]); 
            _builder.Append(" ");
        }
        _msg = _builder.ToString();

        ServerSend.SendChatMessage(_id, _msg);
        Debug.Log($"{Server.clients[_id].player.username}: {_msg}");
    }

    public void CastCommand(int _id, string[] _command)
    {
        // Used for casting spells
        // Checks database for spell
        // Checks if spell is castable
        // Checks target to make sure we can cast it against them
        // Calculates damage + effects
        // Applys damage and effects
        // Sends notification to both clients
    }

    public void SendTellCommand(int _id, string[] _command)
    {
        // Used for whispering
        string _msg = "";
        StringBuilder _builder = new StringBuilder();
        int _toID = 0;

        for (int i = 2; i < _command.Length; i++)
        {
            _builder.Append(_command[i]);
            _builder.Append(" ");
        }
        _msg = _builder.ToString();


        try
        {
            for (int i = 1; i <= Server.clients.Count; i++)
            {
                if (Server.clients[i].player != null)
                {
                    if (Server.clients[i].player.username.ToLower() == _command[1].ToLower())
                    {

                        _toID = Server.clients[i].id;
                    }
                }
            }
        }
        catch (Exception)
        {

            Debug.Log($"User {_command[1]} not found!");
        }
        if(_toID == _id)
        {
            ServerSend.SendTellMessage(0, _id, "You can't message yourself");
            return;
        }

        Debug.Log($"{Server.clients[_id].player.username} says: {_msg} to {Server.clients[_toID].player.username}");

        ServerSend.SendTellMessage(_id,_toID,_msg);
    }

    #endregion
    #region ModCommands
    public void KickCommand(int _id, string[] _command)
    {
        // Used for kicking players
        // GOTTA CHECK FOR PERMISION FIRST!!!!!
    }
    #endregion
    #region AdminCommands
    public void AddSpellCommand(int _id, string[] _command)
    {
        // Used for adding spells to player
        // Again permission
    }
    #endregion
    #endregion


}
