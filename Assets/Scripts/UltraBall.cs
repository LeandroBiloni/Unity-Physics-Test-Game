using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraBall : Ball, IBall
{
    public float force;
    public AudioSource source;
    public float _time;
    public bool _timer;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        source = GetComponent<AudioSource>();
        damage = 3;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Mouse1))
            Ability();
        if (_timer)
            Timer();
    }

    public void Movement()
    {
        if (moving)
            manager.CanClick(false);
        else manager.CanClick(true);
    }

    public void Ability()
    {
        radius = 3;
        damage = 4;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        if (colliders != null)
            foreach (var enemy in colliders)
            {
                if (enemy.gameObject.layer == LayerMask.NameToLayer("Object") || enemy.gameObject.layer == LayerMask.NameToLayer("Poke")) 
                {

                    print("hago fuerza");
                    Rigidbody2D body = enemy.gameObject.GetComponent<Rigidbody2D>();
                    Vector2 dir = enemy.transform.position - transform.position;
                    body.AddForce(force * dir, ForceMode2D.Force);
                    DoDamage(enemy.gameObject);
                }
            }
        _timer = true;
        source.Play();
        transform.localScale = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Object") || collision.gameObject.layer == LayerMask.NameToLayer("Poke"))
        {
            collided = collision.gameObject;
            DoDamage(collided);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Limit"))
        {
            manager.CanClick(true);
            manager.canSpawn = true;
            Destroy(gameObject);
        }
    }

    public void DoDamage(GameObject where)
    {
        manager.Damage(where, damage);
    }

    private void Timer()
    {
        _time += Time.deltaTime;
        if (_time >= 0.5f)
        {
            manager.CanClick(true);
            manager.canSpawn = true;
            Destroy(gameObject);
        }
    }
}
