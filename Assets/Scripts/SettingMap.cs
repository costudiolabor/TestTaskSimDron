using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
[CreateAssetMenu(fileName = "SettingMap", menuName = "Scriptable Objects/SettingMap")]
public class SettingMap : ScriptableObject {
    [SerializeField] private int maxResources = 10;
    [SerializeField] private int maxDrones = 2;
    [SerializeField] private float spawnRadiusResources = 50f;
    [SerializeField] private float spawnRadiusDrones = 1.0f;
    [SerializeField] private float spawnInterval = 50f;
    [SerializeField] private Map map;
    [SerializeField] private ResourceHandler resourcePrefab;
    [SerializeField] private DroneHandler dronePrefab;
    public Map GetMap => map;
    public ResourceHandler ResourcePrefab => resourcePrefab;
    public DroneHandler DronePrefab => dronePrefab;
    public int MaxResources => maxResources;
    public int MaxDrones => maxDrones;
    public float SpawnRadiusResources => spawnRadiusResources;
    
    public float SpawnRadiusDrones => spawnRadiusDrones;
    public float SpawnInterval => spawnInterval;
}