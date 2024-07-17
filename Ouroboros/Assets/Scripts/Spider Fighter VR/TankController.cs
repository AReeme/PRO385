using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TankController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Transform TankMuzzle;
    [SerializeField] public Transform MachineGunMuzzle;
    [SerializeField] public float MachineGunDamage = 5;
    [SerializeField] public float FlameDamage = 10;
    [SerializeField] public float machineGunRange = 500;
    [SerializeField] public GameObject TankMissle;
    private int weaponType = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        switch (weaponType) 
        {
            case 0:
                RaycastHit hit;
				if (Physics.Raycast(MachineGunMuzzle.position, MachineGunMuzzle.forward, out hit, machineGunRange))
				{
                    Debug.Log("Machine Gun Bullet Hit");
                    if(hit.transform.tag == "Enemy")
                    {
                        GameObject enemy = hit.transform.gameObject;

                        //enemy.GetComponent<EnemyAI>()
                    }
                }
				break;
            case 1:
				RaycastHit hit2;
				if (Physics.Raycast(MachineGunMuzzle.position, MachineGunMuzzle.forward, out hit2, machineGunRange))
				{
                    Debug.Log("Flame hit");
					if (hit2.transform.tag == "Enemy")
					{
						GameObject enemy = hit2.transform.gameObject;

						//enemy.GetComponent<EnemyAI>
					}
				}
				break;
            case 2:
                Debug.Log("Missle Shot");
                Instantiate(TankMissle, TankMuzzle.transform);
                break;
            default:
                break;
        }
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
}
