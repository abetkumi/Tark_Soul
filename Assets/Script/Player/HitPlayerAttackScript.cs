using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HitPlayerAttackScript : MonoBehaviour
{


    private GameObject _player;
    private PlayerScript _playerScript;

    private void Start()
    {
        _player = transform.root.gameObject;
        _playerScript = _player.GetComponent<PlayerScript>();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            _playerScript.PlaySE();

            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            if(damageable != null)
            {
                damageable.ReceivedDamage(1);
            }
        }
    }
}
