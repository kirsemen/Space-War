using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UINetworkManager : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(1080/2, 1960/2, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
            if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
            if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
        }

        GUILayout.EndArea();
    }

}
