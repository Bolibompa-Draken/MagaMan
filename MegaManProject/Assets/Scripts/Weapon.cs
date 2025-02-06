using UnityEngine;
using System.Collections;

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

    [Header("Fire Rate Settings")]
    public float fireRate = 1f;
    private float fireTimer = 0f;
    

    private AudioSource audioSource;
    private UnityEngine.Camera mainCamera;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = UnityEngine.Camera.main;
    }

    private void Update()
    {
        OrbitAroundPlayer();
        AimAtMouse();

        
        fireTimer += Time.deltaTime;

        
        if (Input.GetButton("Fire1") && fireTimer >= 1f / fireRate)
        {
            Shoot();
            fireTimer = 0f; 

            audioSource.clip = shootingClip;
            audioSource.Play();
        }

        if (Input.GetButton("Fire2") && chargeTime < 1)
        {
            chargeTime += Time.deltaTime * chargeSpeed;
        }

        if (Input.GetButtonUp("Fire2") && chargeTime >= 1)
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

        Vector3 aimDirection = (mousePosition - firePoint.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, angle);

       
        if ((mousePosition.x < player.position.x && player.localScale.x > 0) ||
            (mousePosition.x > player.position.x && player.localScale.x < 0))
        {
           
            player.localScale = new Vector3(-player.localScale.x, player.localScale.y, player.localScale.z);
        }
    }




    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void ReleaseCharge()
    {
        Instantiate(chargeBulletPrefab, firePoint.position, firePoint.rotation);
        chargeTime = 0;
    }
}
