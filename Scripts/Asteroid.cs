using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _RotationSpeed = 35.0f;
    [SerializeField] private GameObject _Explosion;
    private SpawnManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward*_RotationSpeed*Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag=="Laser")
        {
            Instantiate(_Explosion,gameObject.transform.position,gameObject.transform.rotation);
            manager.Spawn();
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.2f);
        }
    }
}
