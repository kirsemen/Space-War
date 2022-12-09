using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BotNetworkSync : NetworkBehaviour
{
    public NetworkVariable<Vector3> position = new NetworkVariable<Vector3>();
    public NetworkVariable<Quaternion> rotation = new NetworkVariable<Quaternion>();


    private void Update()
    {
        
        if (IsServer)
        {
            position.Value = transform.position;
            rotation.Value = transform.rotation;
        }
        else 
        {
            transform.position = position.Value;
            transform.rotation = rotation.Value;
        }
    }
}

