using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetworkSync : NetworkBehaviour
{

    //public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();


    //public override void OnNetworkSpawn()
    //{
    //    transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
    //    if (IsServer)
    //    {
    //        Position.Value = transform.position;

    //    }
    //    if (IsOwner)
    //    {
    //        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    //        gameObject.GetComponent<RigidbodyFirstPersonController>().enabled = true;
    //    }
    //    GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
    //    GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = false;

    //}

    //[ServerRpc]
    //public void SetServerRpc(Vector3 v)
    //{
    //    if (Vector3.Distance(Position.Value, v) > 1)
    //    {
    //        Position.Value = transform.position;
    //        TpClientRpc(Position.Value);

    //    }
    //    else
    //    {
    //        Position.Value = v;
    //    }

    //}
    //[ClientRpc]
    //public void TpClientRpc(Vector3 v)
    //{
    //    transform.position = v;
    //}
    //void Update()
    //{
    //    if (IsOwner)
    //    {
    //        SetServerRpc(transform.position);
    //    }
    //    else
    //    {
    //        transform.position = Position.Value;
    //    }
    //    if (IsServer)
    //    {
    //        if (Vector3.Distance(Position.Value, transform.position) > 1)
    //            Position.Value = transform.position;
    //    }
    //}
}
