using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankShooting : MonoBehaviour
{
    public Rigidbody m_Shell;

    public Transform m_FireTransform;

    public float m_LaunchForce = 30f;

    public float m_ShootDelay = 1f;

    private bool m_CanShoot;
    private float m_Shoottimer;

    GameObject playerTank;

    private void Awake()
    {
        m_CanShoot = false;
        m_Shoottimer = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_CanShoot == true)
        {
            m_Shoottimer -= Time.deltaTime;
            if (playerTank.activeSelf == true && m_Shoottimer <= 0)
            {
                m_Shoottimer = m_ShootDelay;
                Fire();
            }
        }
    }
    
    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_CanShoot = true;
            playerTank = other.gameObject;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_CanShoot = false;
        }

    }
}
