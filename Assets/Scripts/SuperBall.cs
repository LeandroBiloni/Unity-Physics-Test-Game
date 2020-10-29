using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBall : Ball, IBall
{
    public NormalBall nBall;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        damage = 2;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Mouse1))
            Ability();
    }

    public void Movement()
    {
        if (moving)
            manager.CanClick(false);
        else manager.CanClick(true);
    }

    public void Ability()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject ball = Instantiate(nBall.gameObject, gameObject.transform);
            ball.transform.localScale = new Vector3(ball.transform.localScale.x/2, ball.transform.localScale.y / 2, ball.transform.localScale.z / 2);
            Rigidbody2D rigBody = ball.GetComponent<Rigidbody2D>();
            rigBody.velocity = rb.velocity;
            switch (i)
            {
                case 0:
                    ball.transform.localPosition = new Vector2(0.5f, 0);
                    break;

                case 1:
                    ball.transform.localPosition = new Vector2(0.5f, -2);
                    break;

                case 2:
                    ball.transform.localPosition = new Vector2(0.5f, -4);
                    break;
            }
            ball.transform.parent = null;
            manager.spawnedBall = ball;
        }
        Destroy(gameObject);
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
}
