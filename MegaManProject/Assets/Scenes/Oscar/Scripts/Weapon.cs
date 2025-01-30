using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;
    public float orbitRadius = 1.5f;

    [Header("Fire Points and Prefabs")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject chargeBulletPrefab;
    public CameraShake cameraShake;

    [Header("Charging Settings")]
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float chargeTime;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip chargeClip;
    [SerializeField] private AudioClip shootingClip;

    private bool IsCharging;
    private AudioSource audioSource;
    private UnityEngine.Camera mainCamera;

    [SerializeField] Transform playerTransform;
    [SerializeField] Transform weaponTransfrom;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = UnityEngine.Camera.main;
    }

    private void Update()
    {
        OrbitAroundPlayer();
        AimAtMouse();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            audioSource.clip = shootingClip;
            audioSource.Play();

           // StartCoroutine(cameraShake.Shake(1f, 0.9f));
        }

        if (Input.GetButton("Fire2") && chargeTime < 2)
        {
            IsCharging = true;

            if (IsCharging)
            {
                chargeTime += Time.deltaTime * chargeSpeed;
            }
        }

        if (Input.GetButtonUp("Fire2") && chargeTime >= 2)
        {
            ReleaseCharge();
        }

        if (Input.GetButtonUp("Fire2"))
        {
            chargeTime = 0;
        }
    }

    private void OrbitAroundPlayer()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = (mousePosition - player.position).normalized;
        transform.position = player.position + direction * orbitRadius;
    }

    private void AimAtMouse()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        if (Vector2.Distance(mousePosition, transform.position) > 0.1)
        {
            OrbitAroundPlayer();

         
            if (mousePosition.x < playerTransform.position.x)
            {
                playerTransform.rotation = Quaternion.Euler(0, 180, 0);
                weaponTransfrom.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                playerTransform.rotation = Quaternion.Euler(0, 0, 0);
                weaponTransfrom.localScale = new Vector3(1, 1, 1); 
            }

            
            Vector3 aimDirection = (mousePosition - firePoint.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void ReleaseCharge()
    {
        Instantiate(chargeBulletPrefab, firePoint.position, firePoint.rotation);
        IsCharging = false;
        chargeTime = 0;
    }
}



