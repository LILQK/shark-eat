using System;
using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int team;
    public float maxSize = 3;
    
    public float size;
    private SpriteRenderer rend;
    private Rigidbody2D rb;
    [SerializeField] private ParticleSystem bubbleExplosion;
    public AudioSource pop;
    public void Init()
    {
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void Update()
    {
        transform.localScale = new Vector3(size, size, size);
    }

    private void OnDestroy()
    {
        var a = Instantiate(bubbleExplosion,transform.position,Quaternion.identity);
    }
}
