using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : MonoBehaviour
{

    public Vector3 Base = new Vector3(1,0,1);

    public Vector3 PatrolPos1 = new Vector3(10, 0, 1);

    public Vector3 PatrolPos2 = new Vector3(1, 0, 10);

    public bool Patrolling = true;

    public float m_CloseDistance = 8f;

    public Transform m_Turret;

    private GameObject m_Player;

    private NavMeshAgent m_NavAgent;

    private Rigidbody m_Rigidbody;

    private bool m_Follow;

    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Follow = false;
    }

    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
    }
    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = true;
            //Debug.Log(m_Follow);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = false;
            //Debug.Log(m_Follow);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* This is so dumb because positions suck.. :(
        if (Patrolling == true)
        {
            m_NavAgent.SetDestination(PatrolPos1);
            m_NavAgent.isStopped = false;
            if (transform.position == PatrolPos1)
            {
                m_NavAgent.SetDestination(PatrolPos2);
                m_NavAgent.isStopped = false;
                return;
            }
            if (transform.position == PatrolPos2)
            {
                m_NavAgent.SetDestination(PatrolPos1);
                m_NavAgent.isStopped = false;
                return;
            }
            return;
        }*/

        if (m_Turret != null)
        {
            m_Turret.LookAt(m_Player.transform);
        }

        if (m_Follow == false)
        {
            m_NavAgent.SetDestination(Base);
            m_NavAgent.isStopped = false;
            return;
        }

        float distance = (m_Player.transform.position - transform.position).magnitude;

        //Debug.Log(distance);

        if (m_Follow == true)
        {
            m_NavAgent.SetDestination(m_Player.transform.position);
            m_NavAgent.isStopped = false;
        }
        else
        {
            m_NavAgent.isStopped = true;
        }

    }
}
