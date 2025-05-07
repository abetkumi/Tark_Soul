using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistScript : MonoBehaviour
{
    [SerializeField] BoxCollider m_collider;

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Input.GetButton("Action"))
            {
                m_collider.enabled = false;
            }   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_collider.enabled = true;
        }
    }
}
