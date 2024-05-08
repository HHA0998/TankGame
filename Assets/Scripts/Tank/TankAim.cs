using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankAim : MonoBehaviour
{
    public Transform m_Turret;

    private LayerMask m_LayerMask;

    private void Awake()
    {
        m_LayerMask = LayerMask.GetMask("Ground");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, m_LayerMask))
        {
            m_Turret.LookAt(hit.point);
        }
    }
}
