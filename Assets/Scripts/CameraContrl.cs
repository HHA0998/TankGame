using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraContrl : MonoBehaviour
{
    public float m_DampTime = 0.2f;

    public Transform m_target;

    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;

    private void Awake()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        m_DesiredPosition = m_target.position;
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Camera.main.orthographicSize > 30)
        {
            Camera.main.orthographicSize = 30;
        }
        if (Camera.main.orthographicSize < 1)
        {
            Camera.main.orthographicSize = 1;
        }
        if (Camera.main.orthographicSize <= 30 && Camera.main.orthographicSize >= 1)
        {
            Camera.main.orthographicSize += scroll * 4;
        }
    }
}