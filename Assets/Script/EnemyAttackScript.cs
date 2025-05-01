using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //Debug.Log("ìGÇÃçUåÇÇ™ìñÇΩÇ¡ÇΩÅI");
            IDamageable damageable = col.gameObject.GetComponent<IDamageable>();

            if(damageable != null )
            {
                damageable.ReceivedDamage(1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
