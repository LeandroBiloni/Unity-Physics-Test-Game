using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngineInternal;

public class Ball : MonoBehaviour
{
    public Vector2 originalPos;
    public Rigidbody2D rb;
    public bool moving;
    public GameObject mng;
    public GameManager manager;
    public bool canDestroy;
    public float damage;
    public GameObject collided;
    public float radius;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        mng = GameObject.Find("GameManager");
        manager = mng.GetComponent<GameManager>();
        canDestroy = false;
        rb = GetComponent<Rigidbody2D>();
        originalPos = transform.position;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (rb.velocity != Vector2.zero)
        {
            moving = true;
            manager.CanClick(false);
            manager.canSpawn = false;
            canDestroy = true;
        }


        if (rb.velocity.x <= 0)
        {
            moving = false;
        }

        if (moving == false && canDestroy)
        {
            manager.CanClick(true);
            manager.canSpawn = true;
            Destroy(gameObject);
        } 
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, 0), radius);
    }


}
