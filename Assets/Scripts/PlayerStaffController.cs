using UnityEngine;

public class PlayerStaffController : MonoBehaviour
{
    [SerializeField] Projectile _projectile;
    [SerializeField] AudioClip _shootSound;
    [SerializeField] Transform _tip;
    [SerializeField] float _fireRate;
    [SerializeField] Projectile _specialProjectile;   
    [SerializeField] AudioClip _specialShootSound;   
    [SerializeField] float _specialFireRate = 1f;    
    float _nextSpecialFireTime;

    float _nextFireTime;
    Vector2 _lookDirection;

    void Update()
    {
        SetLookDirection();
        RotateStaff();

        if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + 1f / _fireRate;
            Shoot();
        }

        if (Input.GetButton("Fire2") && Time.time >= _nextSpecialFireTime)
        {
            _nextSpecialFireTime = Time.time + 1f / _specialFireRate;
            ShootSpecial();
        }
    }

    void RotateStaff()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = (mousePosition - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Shoot()
    {
        AudioManager.Instance.PlayAudio(_shootSound, AudioManager.SoundType.SFX, 0.4f,false);
        Projectile newProjectile = Instantiate(_projectile, _tip.position, Quaternion.identity);
        newProjectile.InitializeProjectile(_lookDirection);
    }

    void ShootSpecial()
    {
        AudioManager.Instance.PlayAudio(_specialShootSound, AudioManager.SoundType.SFX, 0.4f,false);
        Projectile newProjectile = Instantiate(_specialProjectile, _tip.position, Quaternion.identity);
        newProjectile.InitializeProjectile(_lookDirection);
    }

    void SetLookDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _lookDirection = (mousePosition - (Vector2)transform.position).normalized;
    }
}
