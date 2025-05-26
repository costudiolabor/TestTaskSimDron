using System.Collections;
using UnityEngine;

public class DroneHandler : MonoBehaviour {
    [SerializeField] private Renderer rendererObject;
    [SerializeField] private float range = 4.0f;
    [SerializeField] private float moveSpeed = 5f;
    private ResourceHandler _currentTarget;
    private ResourceHandler[] _recources;
    private Transform _baseTransform;
    private DroneState _state;
    private Vector3 _startPosition;

    public void Initialize(Color color, Transform baseTransform) {
        _startPosition = transform.position;
        rendererObject.material.color = color;
        _baseTransform = baseTransform;
        _state = DroneState.FindToTarget;
    }

    public void Tick() {
        switch (_state) {
            case DroneState.ReturnStartPosition:
                ReturnStartPosition();
                break;
            
            case DroneState.MoveToTarget:
                MoveToTarget();
                break;
            
            case DroneState.ReturnBase:
                ReturnBase();
                break; 
            
            case DroneState.FindToTarget:
                FindTarget();
                break;
        }
        
    }
    
    public void SetTargets(ResourceHandler[] recources) { _recources = recources; }
    
    private void FindTarget() {
        _currentTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (ResourceHandler resource in _recources) {
            if (resource.gameObject.activeInHierarchy == false || resource.IsReserve == true) continue;
            float distance = Vector3.Distance(resource.transform.position, transform.position);
            Debug.DrawLine(transform.position, resource.transform.position, Color.red, 0.1f);
            if (distance < closestDistance && distance <= range) {
                closestDistance = distance;
                _currentTarget = resource;
                _currentTarget.IsReserve = true;
                _state = DroneState.MoveToTarget;
            }
        }
    }

    private void MoveToTarget() {
        Vector3 targetPosition = _currentTarget.transform.position;
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        Debug.DrawLine(transform.position, targetPosition, Color.green, 0.01f);
        if (Vector3.Distance(transform.position, targetPosition) < 1.0f) {
            Debug.Log("Target Reached");
            StartCoroutine(CollectResource());
        }
    }
    
    private IEnumerator CollectResource() {
        Debug.Log("Collecting resource");
        yield return new WaitForSeconds(2.0f);
       _currentTarget.IsReserve = false;
        _state = DroneState.ReturnBase;
        _currentTarget.gameObject.SetActive(false);
    }
    
    private void ReturnBase() {
        Vector3 targetPosition = _baseTransform.transform.position;
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        Debug.DrawLine(transform.position, targetPosition, Color.green, 0.01f);
        if (Vector3.Distance(transform.position, targetPosition) < 1.0f) {
            Debug.Log("Base Reached");
            StartCoroutine(UnloadingResources());
        }
    }
    
    private IEnumerator UnloadingResources() {
        Debug.Log("UnloadingResources");
        yield return new WaitForSeconds(2.0f);
        _state = DroneState.ReturnStartPosition;
        
    }
    private void ReturnStartPosition() {
        Vector3 targetPosition = _startPosition;
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        Debug.DrawLine(transform.position, targetPosition, Color.green, 0.01f);
        if (Vector3.Distance(transform.position, targetPosition) < 1f) {
            Debug.Log("StartPosition ");
            _state = DroneState.FindToTarget;
        }
    }
}