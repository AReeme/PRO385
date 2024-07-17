using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletDamage = 5;

    void OnColliderEnter(Collider other)
    {
        //player.health -= bulletDamage
        Destroy(this.gameObject);
    }
}
