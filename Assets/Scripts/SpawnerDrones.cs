using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class SpawnerDrones {
    private PoolObjects<DroneHandler> _resourcePool;
    private SettingMap _settingMap;
    
    public SpawnerDrones(SettingMap settingMap) {
        _settingMap = settingMap;
    }

    public DroneHandler[] CreateDrones() {
        _resourcePool = new PoolObjects<DroneHandler>(_settingMap.DronePrefab, _settingMap.MaxDrones, false, null);
        return _resourcePool.GetElements();
    }
    
    public void SpawnDrones() {
        int countFraction1 = _settingMap.MaxDrones / 2;
        int countFraction2 = _settingMap.MaxDrones;

        int start = 0;
        int end = countFraction1;
        Transform parent = _settingMap.GetMap.Fractions[0].pointFraction;
        Color color = _settingMap.GetMap.Fractions[0].color;
        Spawn(start, end, parent, color);
        
        start = countFraction1;
        end = countFraction2;
        parent = _settingMap.GetMap.Fractions[1].pointFraction;
        color = _settingMap.GetMap.Fractions[1].color;
        Spawn(start, end, parent, color);
    }

    private void Spawn(int start, int end, Transform parent, Color color) {
        for (int i = start; i < end; i++) {
            DroneHandler drone = _resourcePool.GetFreeElement();
            Vector3 spawnPosition = parent.position + GetRandomPoint();
            drone.transform.position = spawnPosition;
            drone.Initialize(color, parent);
            drone.gameObject.SetActive(true);
        }

    }
    
    private Vector3 GetRandomPoint() {
            return new Vector3(Random.Range(-_settingMap.SpawnRadiusDrones, _settingMap.SpawnRadiusDrones), 
                Random.Range(0, _settingMap.SpawnRadiusDrones), Random.Range(0, _settingMap.SpawnRadiusDrones));
    }
}
