using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    [SerializeField] private float horizontalMove = 0f;
    private Vector3 m_Velocity = Vector3.zero;

    [SerializeField] private float runSpeed = 0f;

    [Range(0, .3f)]
    [SerializeField] private float m_MovementSmoothing;

    [SerializeField] private SpriteRenderer m_renderer;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;

    private void Update()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        m_renderer.flipX = horizontalMove < 0f ? true : false;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        Move(horizontalMove * Time.fixedDeltaTime);
    }


    public void Move(float move)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }
}

