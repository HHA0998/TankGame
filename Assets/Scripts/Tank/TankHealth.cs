using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TankHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public float m_StartingHealth = 100f;

    public GameObject m_ExplosionPrefab;

    public float m_CurrentHealth;
    private bool m_Dead;

    private ParticleSystem m_ExplosionParticles;

    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
    }
    void Start()
    {
        
    }
    public void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount;
        Debug.Log(m_CurrentHealth);
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }
    private void OnDeath()
    {
        m_Dead = true;

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        m_ExplosionParticles.Play();

        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
