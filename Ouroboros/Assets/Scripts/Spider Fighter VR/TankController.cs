using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] public GameObject shieldBullet;
    [SerializeField] public TextMeshProUGUI HP;
    [SerializeField] public TextMeshProUGUI flameAmmoCount;
    [SerializeField] public TextMeshProUGUI missleAmmoCount;
    [SerializeField] public TextMeshProUGUI batteryCount;
    private bool canShoot = true;
    private bool canShield = true;
    private int weaponType = 0;
    public float firerate = .5f;

    void Start()
    {
        HP.text = health.ToString();
        flameAmmoCount.text = flamethrowerFuel.ToString();
        missleAmmoCount.text = missles.ToString();
        batteryCount.text = battery.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        HP.text = health.ToString();
    }

    public void Fire()
    {
        if (canShoot)
        {
            switch (weaponType)
            {
                case 0:
                    Debug.Log("machine gun shot");
                    Instantiate(MachineGunBullet, TankMuzzle.position, TankMuzzle.rotation);
                    canShoot = false;
                    StartCoroutine("ResetCanShoot", firerate);
                    break;
                case 1:
                    if (flamethrowerFuel > 0)
                    {
                        flamethrowerFuel -= 1;
                        flameAmmoCount.text = flamethrowerFuel.ToString();
                        Debug.Log("flame shot");
                        Instantiate(FireBullet, TankMuzzle.position, TankMuzzle.rotation);
                        canShoot = false;
                        StartCoroutine("ResetCanShoot", firerate);
                    }
                    break;
                case 2:
                    if (missles > 0)
                    {
                        missles -= 1;
                        missleAmmoCount.text = missles.ToString();
                        Debug.Log("Missle Shot");
                        Instantiate(TankMissle, TankMuzzle.position, TankMuzzle.rotation);
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

	IEnumerator ResetCanShield()
	{
		yield return new WaitForSeconds(5f);
		canShield = true;
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

    public void SpawnShield()
    {
        if(canShield && battery > 0)
        {
            Instantiate(shieldBullet, gameObject.transform);
            canShield = false;
            battery -= 1;
            batteryCount.text = battery.ToString();
            Debug.Log(battery);
            StartCoroutine("ResetCanShield", 5f);
        }
    }

    public void GetFuelAmmo()
    {
        flamethrowerFuel += 25;
        if (flamethrowerFuel > 150)
        {
            flamethrowerFuel = 150;
        }
        flameAmmoCount.text = flamethrowerFuel.ToString();

    }

    public void GetMissleAmmo()
    {
        missles += 5;
        if(missles > 15)
        {
            missles = 15;
        }
        missleAmmoCount.text = missles.ToString();
    }

    public void GetBattery()
    {
        battery += 1;
        if(battery > 5)
        {
            battery = 5;
        }
        batteryCount.text = battery.ToString();
    }

    public void UpdateHealthUI()
    {
        HP.text = health.ToString();
    }

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Flame"))
		{
			GetFuelAmmo();
			Destroy(other.gameObject);
		}
		else if (other.gameObject.CompareTag("Missle"))
		{
			GetMissleAmmo();
			Destroy(other.gameObject);
		}
		else if (other.gameObject.CompareTag("Battery"))
		{
			GetBattery();
			Destroy(other.gameObject);
		}

		Debug.Log(other.gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Flame"))
		{
			GetFuelAmmo();
			Destroy(other.gameObject);
		}
		else if (other.gameObject.CompareTag("Missle"))
		{
			GetMissleAmmo();
			Destroy(other.gameObject);
		}
		else if (other.gameObject.CompareTag("Battery"))
		{
			GetBattery();
			Destroy(other.gameObject);
		}

		Debug.Log(other.gameObject);
	}
}
