﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TankHealth : NetworkBehaviour
{
    public float m_StartingHealth = 100f;          
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
    public GameObject m_ExplosionPrefab;


    private AudioSource m_ExplosionAudio;
    private ParticleSystem m_ExplosionParticles;
    [SyncVar (hook = "SetHealthUI") ] public float m_CurrentHealth;
    public bool m_Dead;


    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI(m_CurrentHealth);
    }


    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        if (isServer)
        {
            m_CurrentHealth -= amount;
            if (m_CurrentHealth < 0 && !m_Dead)
            {
                OnDeath();
            }
            SetHealthUI(m_CurrentHealth);
        }

    }
    void Update()
    {
      
    }

    public void SetHealthUI(float heal)
    {
        m_CurrentHealth = heal;

        // Adjust the value and colour of the slider.
        m_Slider.value = m_CurrentHealth;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);

        if (m_CurrentHealth < 0 && !m_Dead)
        {
            OnDeath();
        }
    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        gameObject.SetActive(false);
    }
}