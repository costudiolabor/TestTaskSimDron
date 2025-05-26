using UnityEngine;

public class Entry : MonoBehaviour {
    [SerializeField] private SettingMap settingMap;
    [SerializeField] private Gameplay gameplay;
    [SerializeField] private SpawnerResource spawnerResource;
    [SerializeField] private SpawnerDrones spawnerDrones;

    private Map _map;
    private ResourceHandler[] _resourceHandlers;
    private DroneHandler[] _droneHandlers;
    private void Awake() {
        Initialize();
    }
    
    private void Initialize() {
        CreateMap();
        CreateResources();
        CreateDrones();
        gameplay.Initialize(settingMap.SpawnInterval);
        gameplay.SetResource(_resourceHandlers);
        gameplay.SetDrones(_droneHandlers);
        SetTargetForDrones();
        spawnerDrones.SpawnDrones();
        Subscription();
        Run();
    }

    private void CreateMap() {
        _map = Instantiate(settingMap.GetMap);
    }

    private void CreateResources() {
        spawnerResource = new SpawnerResource(settingMap);
        _resourceHandlers = spawnerResource.CreateResources();
    }
    
    private void CreateDrones() {
        spawnerDrones = new SpawnerDrones(settingMap);
        _droneHandlers = spawnerDrones.CreateDrones();
    }

    private void Run() {
        gameplay.Run();
    }
    
    public void SetTargetForDrones() {
        for (int i = 0; i  < _droneHandlers.Length; i ++) {
            _droneHandlers[i].SetTargets(_resourceHandlers);
        }
    }
    
    private void OnSpawn() {
        spawnerResource.Spawn();
    }

    private void Subscription() {
        gameplay.SpawnEvent += OnSpawn;
    }
    private void UnSubscription() {
        gameplay.SpawnEvent -= OnSpawn;
    }
    private void OnDestroy() => UnSubscription();
    
}
