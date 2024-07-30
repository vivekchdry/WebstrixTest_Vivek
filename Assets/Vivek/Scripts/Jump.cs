using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rigidBody2D;

    [SerializeField] private int jumpPower;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float jumpTime;
    private bool isJumping;
    private float jumpsCount;

    [SerializeField] private LayerMask groundLayer;                          // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;

    [SerializeField] private float fallMultiplier;
    [SerializeField] private Vector2 vecGravity;

    private void Start()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        vecGravity = new Vector2(0, -Physics2D.gravity.y);

    }
    private void Update()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            m_rigidBody2D.velocity = new Vector2(m_rigidBody2D.velocity.x, jumpPower);
            isJumping = true;
            jumpsCount = 0;
        }
        if (m_rigidBody2D.velocity.y > 0 && isJumping)
        {
            jumpsCount += Time.deltaTime;
            if (jumpsCount > jumpTime)
            {
                isJumping = false;
            }
            float currentTime = jumpsCount / jumpTime;
            float currentJumpMultiplier = jumpMultiplier;
            if (currentTime > 0.5f)
            {
                currentJumpMultiplier = currentJumpMultiplier * (1 - currentTime);
            }

            m_rigidBody2D.velocity += vecGravity * jumpMultiplier * Time.deltaTime;
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpsCount = 0;
            if (m_rigidBody2D.velocity.y > 0)
            {
                m_rigidBody2D.velocity = new Vector2(m_rigidBody2D.velocity.x, m_rigidBody2D.velocity.y * 0.6f);
            }
        }
        if (m_rigidBody2D.velocity.y < 0)
        {
            m_rigidBody2D.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
    }
    private bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.1f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
}