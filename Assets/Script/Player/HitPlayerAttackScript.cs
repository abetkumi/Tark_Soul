using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HitPlayerAttackScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            GetComponent<AudioSource>().Play();

            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            if(damageable != null)
            {
                damageable.ReceivedDamage(1);
            }
        }
    }
}
