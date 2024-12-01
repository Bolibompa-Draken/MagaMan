using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject chargeBulletPrefab;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float chargeTime;
    [SerializeField] private AudioClip chargeClip;
    [SerializeField] private AudioClip shootingClip;

    private bool IsCharging;
   
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            audioSource.clip = shootingClip;
            audioSource.Play();
        }
        if (Input.GetButton("Fire1") && chargeTime < 2)
        {
            IsCharging = true;
           
            if (IsCharging == true)
            { 
               
                chargeTime += Time.deltaTime * chargeSpeed;
            }
           
        

        }

        if (Input.GetButtonUp("Fire1") && chargeTime >= 2)

        {
            ReleaseCharge();
        }

        if (Input.GetButtonUp("Fire1"))

        {
            chargeTime = 0;
        }



        void Shoot()
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        }
        void ReleaseCharge () 
        {
            Instantiate(chargeBulletPrefab, firePoint.position, firePoint.rotation);
            IsCharging =false;
            chargeTime = 0;
        }


    }
}
