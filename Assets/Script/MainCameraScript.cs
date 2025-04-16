using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    [SerializeField] GameObject m_trackingTarget; //追跡対象
    Vector3 m_targetToVec; //追跡対象からカメラへのベクトル


    // Start is called before the first frame update
    void Start()
    {
        m_targetToVec = new Vector3(0.0f, 5.0f, -5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //追跡
        //transform.position = Vector3.Lerp(
        //    transform.position,
        //    m_trackingTarget.transform.position + m_targetToVec,
        //    0.05f);


        //追跡
        //transform.position = m_trackingTarget.transform.position + m_targetToVec;

        //カメラの回転
        transform.RotateAround(m_trackingTarget.transform.position,Vector3.up, 20.0f * Time.deltaTime);

        //追跡対象の方を向く
        //transform.LookAt(m_trackingTarget.transform.position);

        
    }
}
