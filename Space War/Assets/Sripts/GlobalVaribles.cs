using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVaribles : MonoBehaviour
{
    public static bool IsPLayerTeam1;
    public static bool IsPLayerTeam2;
    
    public static GameObject PrefabTeam1;
    public static GameObject PrefabTeam2;
    
    public static GameObject SpawnPointTeam1;
    public static GameObject SpawnPointTeam2;

    [SerializeField]
    private GameObject _PrefabTeam1;
    [SerializeField]
    private GameObject _PrefabTeam2;
    [SerializeField]
    private GameObject _SpawnPointTeam1;
    [SerializeField]
    private GameObject _SpawnPointTeam2;

    private void Start()
    {
        IsPLayerTeam1 = false;
        IsPLayerTeam2=false;
    }

    private void OnValidate()
    {
        PrefabTeam1 = _PrefabTeam1;
        PrefabTeam2 = _PrefabTeam2;
        SpawnPointTeam1 = _SpawnPointTeam1;
        SpawnPointTeam2 = _SpawnPointTeam2;
    }
}
