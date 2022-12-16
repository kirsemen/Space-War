using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameController : MonoBehaviour
{
    bool Inited=false;
    private void Update()
    {
        if (NetworkManager.Singleton.IsServer&&!Inited)
        {
            Inited=true;
            var go = Instantiate(GlobalVaribles.PrefabBotController);
            go.GetComponent<NetworkObject>().Spawn();
        }
    }
}
