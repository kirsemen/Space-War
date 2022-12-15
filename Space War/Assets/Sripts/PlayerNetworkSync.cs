using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public enum PlayerTeam
{
    None,
    Team1,
    Team2
}

public class PlayerNetworkSync : NetworkBehaviour
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
            parametrId.Value = ParametrId.GetNewId();
        }
        if (IsServer && IsOwner)
        {

            GameObject go = Instantiate(UI.main.myPrefab);
            go.GetComponent<NetworkObject>().Spawn();
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
        var go = Instantiate(GlobalVaribles.PrefabPlayerTeam1);
        go.transform.parent = transform;
        go.transform.GetChild(0).position = GlobalVaribles.SpawnPointPlayerTeam1.transform.position;
        go.transform.GetChild(1).position = GlobalVaribles.SpawnPointPlayerTeam1.transform.position;
        go.transform.GetChild(2).position = GlobalVaribles.SpawnPointPlayerTeam1.transform.position;
        go.transform.GetChild(0).rotation = GlobalVaribles.SpawnPointPlayerTeam1.transform.rotation;
        if (IsOwner)
        {
            UI.SetActive(true);
            Camera.main.gameObject.SetActive(false);
            transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

    }
    public void SpawnSpaceshipTeam2()
    {
        var go = Instantiate(GlobalVaribles.PrefabPlayerTeam2);
        go.transform.parent = transform;
        go.transform.GetChild(0).position = GlobalVaribles.SpawnPointPlayerTeam2.transform.position;
        go.transform.GetChild(1).position = GlobalVaribles.SpawnPointPlayerTeam2.transform.position;
        go.transform.GetChild(2).position = GlobalVaribles.SpawnPointPlayerTeam2.transform.position;
        go.transform.GetChild(0).rotation = GlobalVaribles.SpawnPointPlayerTeam2.transform.rotation;
        if (IsOwner)
        {
            UI.SetActive(true);
            Camera.main.gameObject.SetActive(false);
            transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

    }

}
