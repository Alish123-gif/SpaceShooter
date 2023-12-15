using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    private const float _delay = 2.5f;
    private Animator _enemyAnimator;
    private Player _player;
    private BoxCollider2D _boxCollider;
    private AudioSource _audioSource;

    void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }

    void OnBecameInvisible()
    {
        transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 13, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 0.2f;

            _player.Damage();
            _audioSource.enabled = true;
            _boxCollider.enabled = false;
            Destroy(gameObject, _delay);
        }
        else if (other.CompareTag("Shield"))
        {
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 0.2f;
            Destroy(other.gameObject);
            _audioSource.enabled = true;
            _boxCollider.enabled = false;
            Destroy(gameObject, _delay);
        }
        else if (other.CompareTag("Laser"))
        {
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 0.2f;

            if (_player != null)
            {
                _player.addScore(10);
            }
            _audioSource.enabled = true;
            _boxCollider.enabled = false;
            Destroy(gameObject, _delay);
            Destroy(other.gameObject);
        }
    }
}
