using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int _powerUpId; // 0 - Flamethrower Ammo, 1 - Missile Ammo, 2 - Battery

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Player player = other.transform.GetComponent<Player>();

            if (_powerUpId == 0)
            {
                // player.GetFlameAmmo();
            } else if (_powerUpId == 1)
            {
                //player.GetMissileAmmo();
            } else
            {
                //player.GetBattery();
            }
        }

        Destroy(this.gameObject);
    }
}
