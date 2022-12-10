using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCollider : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        Physics.IgnoreLayerCollision(0, 3);
    }

    void Update()
    {
        
    }
}
