using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;

public class SpaghettiGameManager : NetworkBehaviour
{
    static public List<NetworkSpaghetti> spaghettis = new List<NetworkSpaghetti>();
    static public SpaghettiGameManager sInstance = null;
    static public int counter = 0;

    void Awake()
    {
        sInstance = this;
    }

    void Start()
    {
    }

    [ServerCallback]
    void Update()
    {
    }

}

