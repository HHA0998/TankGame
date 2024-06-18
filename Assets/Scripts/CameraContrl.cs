using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContrl : MonoBehaviour
{
    public float m_DampTime = 0.3f;
    public float menuMoveSpeed = 1.0f; 
    public float unitsMaxMove = 5.0f; 

    private Transform m_target; 

    private Vector3 m_movingvelocity;
    private Vector3 m_Togoto;

    private bool followPlayer = false; 

    private void Awake()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (followPlayer && m_target != null)
        {
            MoveToTarget();
        }
        else
        {
            MoveInMenu();
        }
    }

    private void MoveToTarget()
    {
        m_Togoto = m_target.position;
        transform.position = Vector3.SmoothDamp(transform.position, m_Togoto, ref m_movingvelocity, m_DampTime); //i love smooth damp
    }

    private void MoveInMenu()
    {
        float Movement = Mathf.Sin(Time.time * menuMoveSpeed) * unitsMaxMove;
        float zOffset = Mathf.Cos(Time.time * menuMoveSpeed) * unitsMaxMove; //using cosine to offset so i get a nice smooth offset :)

        m_Togoto = new Vector3(Movement, transform.position.y, Movement + zOffset);
        transform.position = Vector3.SmoothDamp(transform.position, m_Togoto, ref m_movingvelocity, m_DampTime);
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            m_target = GameObject.FindGameObjectWithTag("Player").transform; //Because were spawning new clones
            followPlayer = true;
        }

        else
        {
            followPlayer = false;
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + scroll * 4, 1f, 30f); //Revised scrollwell
    }
}
