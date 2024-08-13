using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TankController : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 5;
    public int flamethrowerFuel = 150;
    public int missles = 15;
    public int battery = 5;
    [SerializeField] public Transform TankMuzzle;
    [SerializeField] public Transform MachineGunMuzzle;
    [SerializeField] public float MachineGunDamage = 5;
    [SerializeField] public float FlameDamage = 10;
    [SerializeField] public float machineGunRange = 500;
    [SerializeField] public GameObject TankMissle;
    [SerializeField] public GameObject MachineGunBullet;
    [SerializeField] public GameObject FireBullet;
    private bool canShoot = true;
    private int weaponType = 0;
    public float firerate = .5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire()
    {
        if (canShoot)
        {
            switch (weaponType)
            {
                case 0:
                    Debug.Log("machine gun shot");
                    Instantiate(MachineGunBullet, TankMuzzle.transform);
                    canShoot = false;
                    StartCoroutine("ResetCanShoot", firerate);
                    break;
                case 1:
                    if (flamethrowerFuel > 0)
                    {
                        flamethrowerFuel -= 1;
                        Debug.Log("flame shot");
                        Instantiate(FireBullet, TankMuzzle.transform);
                        canShoot = false;
                        StartCoroutine("ResetCanShoot", firerate);
                    }
                    break;
                case 2:
                    if (missles > 0)
                    {
                        missles -= 1;
                        Debug.Log("Missle Shot");
                        Instantiate(TankMissle, TankMuzzle.transform);
                        canShoot = false;
                        StartCoroutine("ResetCanShoot", firerate);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void ResetCanShoot()
    {
        canShoot = true;
    }

    public void SwitchToWeaponMachineGun()
    {
        weaponType = 0;
    }

	public void SwitchToWeaponFlameThrower()
	{
		weaponType = 1;
	}

	public void SwitchToWeaponTankMissle()
	{
		weaponType = 2;
	}

    public void Shield()
    {
        //funny code goes brrrr
    }

    public void GetFuelAmmo()
    {
        flamethrowerFuel += 25;
        if (flamethrowerFuel > 150)
        {
            flamethrowerFuel = 150;
        }

    }

    public void GetMissleAmmo()
    {
        missles += 5;
        if(missles > 15)
        {
            missles = 15;
        }
    }

    public void GetBattery()
    {
        battery += 1;
        if(battery > 5)
        {
            battery = 5;
        }
    }
}
