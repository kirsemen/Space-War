using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GlobalVaribles : MonoBehaviour
{

    public static bool IsPLayerTeam1;
    public static bool IsPLayerTeam2;
    
    public static GameObject PrefabPlayerTeam1;
    public static GameObject PrefabPlayerTeam2;

    public static GameObject PrefabBotController;

    public static GameObject PrefabBotTeam1;
    public static GameObject PrefabBotTeam2;
    
    public static GameObject SpawnPointPlayerTeam1;
    public static GameObject SpawnPointPlayerTeam2;

    [SerializeField]
    private GameObject _PrefabPlayerTeam1;
    [SerializeField]
    private GameObject _PrefabPlayerTeam2;

    [SerializeField]
    private GameObject _PrefabBotController;

    [SerializeField]
    private GameObject _PrefabBotTeam1;
    [SerializeField]
    private GameObject _PrefabBotTeam2;

    [SerializeField]
    private GameObject _SpawnPointPlayerTeam1;
    [SerializeField]
    private GameObject _SpawnPointPlayerTeam2;

    private void Start()
    {
        PrefabPlayerTeam1 = _PrefabPlayerTeam1;
        PrefabPlayerTeam2 = _PrefabPlayerTeam2;

        PrefabBotController = _PrefabBotController;

        PrefabBotTeam1 = _PrefabBotTeam1;
        PrefabBotTeam2 = _PrefabBotTeam2;

        SpawnPointPlayerTeam1 = _SpawnPointPlayerTeam1;
        SpawnPointPlayerTeam2 = _SpawnPointPlayerTeam2;
    }

}
