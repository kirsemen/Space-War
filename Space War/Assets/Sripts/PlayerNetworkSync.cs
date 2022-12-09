using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;



public class PlayerNetworkSync : NetworkBehaviour
{
    public enum PlayerTeam
    {
        None,
        Team1,
        Team2
    }

    
    public NetworkVariable<Vector3> position = new NetworkVariable<Vector3>();
    public NetworkVariable<Quaternion> rotation = new NetworkVariable<Quaternion>();
    public NetworkVariable<ParametrsSync> parametrsSync = new NetworkVariable<ParametrsSync>();
    public NetworkVariable<PlayerTeam> Team = new NetworkVariable<PlayerTeam>(PlayerTeam.None);
    private bool Initiated = false;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            if (!GlobalVaribles.IsPLayerTeam1)
            {
                GlobalVaribles.IsPLayerTeam1 = true;
                Team.Value = PlayerTeam.Team1;
            }
            else if (!GlobalVaribles.IsPLayerTeam2)
            {
                GlobalVaribles.IsPLayerTeam2 = true;
                Team.Value = PlayerTeam.Team2;
            }

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
        var go = Instantiate(GlobalVaribles.PrefabTeam1);
        go.transform.parent = transform;
        go.transform.position = GlobalVaribles.SpawnPointTeam1.transform.position;
        go.transform.rotation = GlobalVaribles.SpawnPointTeam1.transform.rotation;
        if (IsOwner)
        {
            Camera.main.gameObject.SetActive(false);
            transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

    }
    public void SpawnSpaceshipTeam2()
    {
        var go = Instantiate(GlobalVaribles.PrefabTeam2);
        go.transform.parent = transform;
        go.transform.position = GlobalVaribles.SpawnPointTeam2.transform.position;
        go.transform.rotation = GlobalVaribles.SpawnPointTeam2.transform.rotation;
        if (IsOwner)
        {
            Camera.main.gameObject.SetActive(false);
            transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

    }

}
