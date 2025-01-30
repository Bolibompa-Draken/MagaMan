using UnityEngine;

public class FireRatePickup : MonoBehaviour
{
    [SerializeField] private float fireRateMultiplier = 4f;
    [SerializeField] private float duration = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weapon playerWeapon = collision.GetComponent<Weapon>();
        if (playerWeapon != null)
        {
            playerWeapon.ActivateFireRatePowerUp();
            Destroy(gameObject);
        }
    }
}
