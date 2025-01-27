using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class SharkCollisions : MonoBehaviour
{
    public string fishTag;
    public string bubbleTag;
    public string trashTag = "Trash";
    public string trashBagTag = "Bag";

    private FishMovement movement;
    private SpriteRenderer rend;
    private Collider2D collider;

    public AudioSource biteSource;
    public AudioSource popSource;
    public Transform yum;
    public Transform pop;
    public Sprite originalSprite;
    public Sprite stunSprite;
    public Sprite bagSprite;
    public Transform stunt;
    private void Awake()
    {
        movement = GetComponent<FishMovement>();
        rend = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        yum.gameObject.SetActive(false);
        yum.parent = null;
        pop.gameObject.SetActive(false);
        pop.parent = null;
        stunt.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(fishTag))
        {
            biteSource.Play();
            Debug.Log("Collided with fish");
            ScoreManager.Instance.AddScore(GameManager.Instance.GetCurrentRole(),1 );
            Destroy(other.gameObject);
            yum.gameObject.SetActive(true);
            yum.position = other.transform.position;
            yum.DOScale(1f, .5f).From(0f).SetEase(Ease.OutBounce).OnComplete(() => yum.gameObject.SetActive(false));
            yum.rotation = Quaternion.Euler(0f,0f,  Random.Range(-45f,45f));
        }

        if (other.CompareTag(trashTag))
        {
            ScoreManager.Instance.RemoveScore(GameManager.Instance.GetCurrentRole(), 1);
            if (other.TryGetComponent<Orca>(out var orca))
            {
                ScoreManager.Instance.RemoveScore(GameManager.Instance.GetCurrentRole(), 2);
                movement.canMove = false;
                collider.enabled = false;
                rend.sprite = stunSprite;
                StartCoroutine(WaitAndReEnable(2f));
                stunt.gameObject.SetActive(true);
                stunt.DOScale(1f, .5f).From(0f).SetEase(Ease.OutBounce).OnComplete(() => stunt.gameObject.SetActive(false));
                stunt.rotation = Quaternion.Euler(0f,0f,  Random.Range(-45f,45f));
            }
        }
        
        if (other.CompareTag(trashBagTag))
        {
            Destroy((other.gameObject));
            movement.isSlowed = true;
            collider.enabled = false;
            rend.sprite = bagSprite;
            StartCoroutine(WaitAndReEnable(2f));
        }

        if (other.CompareTag(bubbleTag))
        {
            if (other.TryGetComponent<Bubble>(out var bubble))
            {
                pop.gameObject.SetActive(true);
                pop.position = other.transform.position;
                pop.DOScale(1f, .5f).From(0f).SetEase(Ease.OutBounce).OnComplete(() => pop.gameObject.SetActive(false));
                pop.rotation = Quaternion.Euler(0f,0f,  Random.Range(-45f,45f));
                popSource.Play();
                movement.canMove = false;
                collider.enabled = false;
                stunt.gameObject.SetActive(true);
                stunt.DOScale(1f, bubble.size * 2f).From(0f).SetEase(Ease.OutBounce).OnComplete(() => stunt.gameObject.SetActive(false));
                stunt.DOShakeRotation(bubble.size * 2f,45f);
                rend.sprite = stunSprite;
                StartCoroutine(WaitAndReEnable(bubble.size * 2f));
                Debug.Log("Collided with bubble" + " " + bubble.size / 2f);
            }
            
            Destroy(other.gameObject);
        }
    }


    IEnumerator WaitAndReEnable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        movement.canMove = true;
        collider.enabled = true;
        movement.isSlowed = false;
        rend.color = Color.white;
        rend.sprite = originalSprite;
    }
}
