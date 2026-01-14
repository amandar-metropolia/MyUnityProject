using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    EntityHealth _entityHealth;
    [SerializeField] AudioClip _deathSound;
    UnityEngine.AI.NavMeshAgent _agent;
    GameObject _target;


    void Awake()
    {
        _entityHealth = GetComponent<EntityHealth>();

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
    }

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _entityHealth.OnDeath += DestroyEnemy;
    }

    void Update()
    {
        _agent.SetDestination(_target.transform.position);
    }

    void OnDisable()
    {
        _entityHealth.OnDeath -= DestroyEnemy;
    }

   public void DestroyEnemy()
    {
        AudioManager.Instance.PlayAudio(_deathSound, AudioManager.SoundType.SFX, 1.0f, false);
        Destroy(gameObject);
    }
}
