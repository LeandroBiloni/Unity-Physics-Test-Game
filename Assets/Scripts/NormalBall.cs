using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : Ball, IBall
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        damage = 1;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Movement();
    }

    public void Ability()
    {
        throw new System.NotImplementedException();
    }

    public void Movement()
    {
        if (moving)
            manager.CanClick(false);
        else manager.CanClick(true);
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
