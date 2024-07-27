using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int _powerUpId; // 0 - Flamethrower Ammo, 1 - Missile Ammo, 2 - Battery

    private void OnCollisionEnter(Collision other)
    {

        
        if (other.gameObject.tag == "Player")
        {
            //Player player = other.transform.GetComponent<Player>();

            if (_powerUpId == 0)
            {
                // player.GetFlameAmmo();
                other.gameObject.GetComponent<TankController>().GetFuelAmmo();
            }
            else if (_powerUpId == 1)
            {
                //player.GetMissileAmmo();
                other.gameObject.GetComponent<TankController>().GetMissleAmmo();
            }
            else
            {
            //player.GetBattery();
            other.gameObject.GetComponent<TankController>().GetBattery();
            }
        }
         Destroy(this.gameObject);
    }
}