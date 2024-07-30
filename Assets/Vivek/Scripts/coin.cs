using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class coin : MonoBehaviour
{
    public float bounceDuration = 1f;
    public float yRange = 0.2f;
    public bool canAnimateUpDown = true;

    private void Start()
    {
        if (canAnimateUpDown)
        {
            Vector3 startPos = transform.position;
            Vector3 upPos = startPos + new Vector3(0f, yRange, 0f);
            Vector3 downPos = startPos - new Vector3(0f, yRange, 0f);
            transform.DOMove(upPos, bounceDuration).SetLoops(-1, LoopType.Yoyo);
            transform.DOMove(downPos, bounceDuration).SetLoops(-1, LoopType.Yoyo).SetDelay(bounceDuration);
        }
    }

    private int scoreValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ScoreKeeper.IncreaseScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
