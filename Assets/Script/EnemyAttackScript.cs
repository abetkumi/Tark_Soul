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
            Debug.Log("“G‚ÌUŒ‚‚ª“–‚½‚Á‚½I");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
