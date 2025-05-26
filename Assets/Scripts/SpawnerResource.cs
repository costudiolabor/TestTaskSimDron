using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class SpawnerResource {
    private PoolObjects<ResourceHandler> _resourcePool;
    private SettingMap _settingMap;
    public SpawnerResource(SettingMap settingMap) {
        _settingMap = settingMap;
    }

    public ResourceHandler[] CreateResources() {
        _resourcePool = new PoolObjects<ResourceHandler>(_settingMap.ResourcePrefab, _settingMap.MaxResources, false, null);
        return _resourcePool.GetElements();
    }
    
    public void Spawn() {
        ResourceHandler resource = _resourcePool.GetFreeElement();
        Vector3 spawnPosition = GetRandomPoint();
        resource.transform.position = spawnPosition;
        resource.gameObject.SetActive(true);
    }
    
    private Vector3 GetRandomPoint() {
        return new Vector3(Random.Range(-_settingMap.SpawnRadiusResources, _settingMap.SpawnRadiusResources), Random.Range(0, _settingMap.SpawnRadiusResources), Random.Range(0, _settingMap.SpawnRadiusResources));
    }
}
