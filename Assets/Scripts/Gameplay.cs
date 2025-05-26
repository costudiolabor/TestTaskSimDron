using System;
using UnityEngine;

public class Gameplay : MonoBehaviour {
    private bool _isRunning;
    private ResourceHandler[] _resources;
    private DroneHandler[] _drones;
    private float _timerSpawn;
    private float _spawnInterval;
       
    public event Action SpawnEvent; 
        
    public void Initialize(float spawnInterval) {
        _spawnInterval = spawnInterval;
    }

    public void SetResource(ResourceHandler[] resourceHandlers) {
        _resources = resourceHandlers;
    }
        
    public void SetDrones(DroneHandler[] drones) {
        _drones = drones;
    }

    public void Run() {
        _isRunning = true;
    }
        
    public void Update() {
        if(_isRunning == false) return; 
        UpdateResources();
        UpdateDrones();
        UpdateSpawn();
    }

    private void UpdateResources() {
        for (int i = 0; i  < _resources.Length; i ++) {
            _resources[i].Tick();
        }
    }
        
    private void UpdateDrones() {
        for (int i = 0; i  < _drones.Length; i ++) {
            _drones[i].Tick();
        }
    }
        
    public void UpdateSpawn() {
        _timerSpawn -= Time.deltaTime;
        if (_timerSpawn <= 0f) {
            Spawn();
            _timerSpawn = _spawnInterval;
        }
    }
        
    public void Spawn() {
        SpawnEvent?.Invoke();
    }
}
