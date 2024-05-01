using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody m_Shell;

    public Transform m_FireTransform;

    public float m_LaunchForce = 30f;

    public float m_shotDelay = 1f;

    public float m_ShootTimer;

    public bool m_canShoot;
    void Start()
    {
        m_canShoot = true;
        m_ShootTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
     if (Input.GetButton("Fire1") && m_canShoot == true)
        {
            m_ShootTimer -= Time.deltaTime;
            if (m_ShootTimer <= 0)
            {
                m_ShootTimer = m_shotDelay;
                Fire();
            }
        }   
    }
    private void Fire()
    {
        Rigidbody ShellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation);

        ShellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
    }
}
