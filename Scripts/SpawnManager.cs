using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _PowerUps;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemy_container;
    [SerializeField] private bool _stopSpawing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        while(!_stopSpawing)
        {
            GameObject newEnemy = Instantiate(_enemy, new Vector3(Random.Range(-8.0f,8.0f),13,0), Quaternion.identity);
            newEnemy.transform.parent = _enemy_container.transform;
            yield return new WaitForSeconds(3);
        }
    }
    IEnumerator SpawnPowerUpRoutine(){
        
        while (!_stopSpawing)
        {
            int powerID = Random.Range(0, 3);
            switch (powerID)
            {
                case 0:
                    yield return new WaitForSeconds(Random.Range(3.0f,8.0f));
                    Instantiate(_PowerUps[0],new Vector3(Random.Range(-8.0f,8.0f),13,0), Quaternion.identity);
                    break;
                case 1:
                    yield return new WaitForSeconds(Random.Range(3.0f,8.0f));
                    Instantiate(_PowerUps[1],new Vector3(Random.Range(-8.0f,8.0f),13,0), Quaternion.identity);
                    break;
                case 2:
                    yield return new WaitForSeconds(Random.Range(3.0f,8.0f));
                    Instantiate(_PowerUps[2],new Vector3(Random.Range(-8.0f,8.0f),13,0), Quaternion.identity);
                    break;
            }
            
        }
    }
    public void OnPlayerDeath(){
        _stopSpawing = true;
    }
    public void Spawn(){
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }
}
