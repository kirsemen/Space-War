using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BotNetworkSync : NetworkBehaviour
{

    public NetworkVariable<Vector3> position = new NetworkVariable<Vector3>();
    public NetworkVariable<Quaternion> rotation = new NetworkVariable<Quaternion>();
    public NetworkVariable<ParametrsSync> parametrsSync = new NetworkVariable<ParametrsSync>();
    public NetworkVariable<PlayerTeam> Team = new NetworkVariable<PlayerTeam>(PlayerTeam.None);
    public NetworkVariable<int> parametrId = new NetworkVariable<int>();

    private bool Initiated = false;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Team.Value = PlayerTeam.Team1;
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
            if (Team.Value == PlayerTeam.None) return;
            Initiated = true;

            if (Team.Value == PlayerTeam.Team1)
                SpawnSpaceshipTeam1();
            else if (Team.Value == PlayerTeam.Team2)
                SpawnSpaceshipTeam2();
            transform.GetChild(0).GetComponent<Parametrs>().id = parametrId.Value;
        }

        if (IsOwner)
        {
            SetServerRpc(transform.GetChild(0).GetChild(0).position, transform.GetChild(0).GetChild(0).rotation, new ParametrsSync(transform.GetChild(0).GetComponent<Parametrs>()));
        }
        else
        {
            transform.GetChild(0).GetChild(0).position = position.Value;
            transform.GetChild(0).GetChild(0).rotation = rotation.Value;
            transform.GetChild(0).GetComponent<Parametrs>().Set(parametrsSync.Value);
        }
    }


    public void SpawnSpaceshipTeam1()
    {
        var go = Instantiate(GlobalVaribles.PrefabBotTeam1);
        go.transform.parent = transform;

    }
    public void SpawnSpaceshipTeam2()
    {
        var go = Instantiate(GlobalVaribles.PrefabBotTeam2);
        go.transform.parent = transform;

    }
}

