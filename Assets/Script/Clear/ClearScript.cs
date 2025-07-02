using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScript : MonoBehaviour
{
    [SerializeField]
    GameObject m_bornfire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClearActive()
    {
        m_bornfire.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Action"))
            {
                Clear();
            }
        }
    }

    void Clear()
    {
        Debug.Log("ÉNÉäÉA");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
