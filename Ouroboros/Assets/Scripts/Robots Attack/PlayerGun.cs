using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public  GameObject playerBullet;
    [SerializeField] Transform muzzlePosition;
    public float damage;
    public float fireRate;
    private bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot()
    {
        if(canShoot == true)
        {
            Instantiate(playerBullet, muzzlePosition.position, muzzlePosition.rotation);
            canShoot = false;
            StartCoroutine("ResetFire", fireRate);
        }
    }

    public void ResetFire()
    {
        canShoot = true;
    }
}
