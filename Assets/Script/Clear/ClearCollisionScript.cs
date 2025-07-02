using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCollisionScript : MonoBehaviour
{
    [SerializeField] GameObject m_clearObject;
    ClearScript m_clearScript;
    [SerializeField] BoxCollider m_collider;

    // Start is called before the first frame update
    void Start()
    {
        m_clearScript = m_clearObject.GetComponent<ClearScript>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Action"))
            {
                m_clearScript.Clear();
                m_collider.enabled = false;
            }
        }
    }
}
