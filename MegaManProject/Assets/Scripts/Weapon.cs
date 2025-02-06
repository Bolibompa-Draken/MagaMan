using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;
    public float orbitRadius = 1.5f;
    public Rigidbody2D playerRB;

    [Header("Fire Points and Prefabs")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject chargeBulletPrefab;
    public CameraShake cameraShake;

    [Header("Charging Settings")]
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float chargeTime;
    private bool isShooting = false;
    private bool isAimingRight = true;


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
        Aim();

        
        fireTimer += Time.deltaTime;

        
        if (Input.GetButton("Fire1") && fireTimer >= 1f / fireRate)
        {
            Shoot();
            fireTimer = 0f; 

            audioSource.clip = shootingClip;
            audioSource.Play();
            Recoil();
        }

        if (Input.GetButton("Fire2") && chargeTime < 1)
        {
            chargeTime += Time.deltaTime * chargeSpeed;
            
        }

        if (Input.GetButtonUp("Fire2") && chargeTime >= 1)
        {
            ReleaseCharge();
            Recoil();
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

    public void Aim() //Looking right or left
    {
        float zRotation = firePoint.transform.rotation.eulerAngles.z; // Get the Z rotation in degrees

        if (zRotation >= 270 || zRotation <= 90)
        {
            isAimingRight = true;
            Debug.Log("Aiming to the right");
        }
        else
        {
            isAimingRight = false;
            Debug.Log("Aiming to the left");
        }
    }
    private void Recoil()
    {
        if (isAimingRight == true && isShooting == true)
        {
            playerRB.AddRelativeForce(Vector2.right * 100, ForceMode2D.Impulse);
        }
        else if (isAimingRight == false && isShooting == true)
        {
            playerRB.AddRelativeForce(Vector2.left * 100, ForceMode2D.Impulse);
        }
    }

}
