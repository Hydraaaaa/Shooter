using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager
{
    public event System.Action onServerConnect;
    public event System.Action onClientConnect;

    public event System.Action onClientDisconnect;
    public event System.Action onServerDisconnect;

    public event System.Action onClientError;

    public event System.Action onStartClient;
    public event System.Action onStartServer;
    public event System.Action onStartHost;

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        if (onServerConnect != null)
        {
            onServerConnect();
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        if (onClientConnect != null)
        {
            onClientConnect();
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        if (onServerDisconnect != null)
        {
            onServerDisconnect();
        }
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        if (onClientDisconnect != null)
        {
            onClientDisconnect();
        }
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);

        if (onClientError != null)
        {
            onClientError();
        }
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);

        if (onStartClient != null)
        {
            onStartClient();
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        if (onStartServer != null)
        {
            onStartServer();
        }
    }

    public override void OnStartHost()
    {
        base.OnStartHost();

        if (onStartHost != null)
        {
            onStartHost();
        }
    }
}
