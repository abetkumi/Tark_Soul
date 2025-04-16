using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    [SerializeField] GameObject m_trackingTarget; //�ǐՑΏ�
    Vector3 m_targetToVec; //�ǐՑΏۂ���J�����ւ̃x�N�g��


    // Start is called before the first frame update
    void Start()
    {
        m_targetToVec = new Vector3(0.0f, 5.0f, -5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //�ǐ�
        //transform.position = Vector3.Lerp(
        //    transform.position,
        //    m_trackingTarget.transform.position + m_targetToVec,
        //    0.05f);


        //�ǐ�
        //transform.position = m_trackingTarget.transform.position + m_targetToVec;

        //�J�����̉�]
        transform.RotateAround(m_trackingTarget.transform.position,Vector3.up, 20.0f * Time.deltaTime);

        //�ǐՑΏۂ̕�������
        //transform.LookAt(m_trackingTarget.transform.position);

        
    }
}
