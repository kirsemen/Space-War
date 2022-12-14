using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static PlayerNetworkSync;

public class BotNetworkSync : NetworkBehaviour
{
    public NetworkVariable<Vector3> position = new NetworkVariable<Vector3>();
    public NetworkVariable<Quaternion> rotation = new NetworkVariable<Quaternion>();
    public NetworkVariable<ParametrsSync> parametrsSync = new NetworkVariable<ParametrsSync>();
    public NetworkVariable<int> parametrId = new NetworkVariable<int>();

    private bool Initiated = false;
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            parametrId.Value = ParametrId.GetNewId();
        }
    }

    [ServerRpc]
    public void SetServerRpc(Vector3 p, Quaternion r, ParametrsSync ps)
    {
        position.Value = p;
        rotation.Value = r;
        parametrsSync.Value = ps;

    }
    private void Update()
    {
        if (!Initiated)
        {
            Initiated = true;
            GetComponent<Parametrs>().id = parametrId.Value;
        }

        if (IsOwner)
        {
            SetServerRpc(transform.GetChild(0).position, transform.GetChild(0).rotation, new ParametrsSync(GetComponent<Parametrs>()));
        }
        else
        {
            transform.GetChild(0).position = position.Value;
            transform.GetChild(0).rotation = rotation.Value;
            GetComponent<Parametrs>().Set(parametrsSync.Value);
        }
    }
}

