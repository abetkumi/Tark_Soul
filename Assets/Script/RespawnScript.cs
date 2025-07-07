using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] GameObject m_playerObject;
    public Vector3 m_respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        m_respawnPoint = m_playerObject.transform.position;
    }

    public void Respawn()
    {
        m_playerObject.transform.position = m_respawnPoint;
    }
}
