using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    public float MaxSpeed = 5f;

    void Awake()
    {
        GameManager.Settings currentSettings = GameManager.Instance.LoadSettings();
        if (currentSettings != null)
        {
            MaxSpeed = currentSettings.ballSpeed;
        }
    }

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > MaxSpeed)
        {
            velocity = velocity.normalized * MaxSpeed;
        }

        m_Rigidbody.velocity = velocity;
    }
}
