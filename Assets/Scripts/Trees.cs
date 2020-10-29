using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    public float hp;
    private Rigidbody2D _rb;
    public float points;
    private GameObject _mng;
    private GameManager _manager;

    // Start is called before the first frame update
    void Start()
    {
        _mng = GameObject.Find("GameManager");
        _manager = _mng.GetComponent<GameManager>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damage)
    {
        if (hp - damage <= 0)
            _manager.AddPoints(points * hp);
        else _manager.AddPoints(points * damage);

        hp -= damage;
        if (hp <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Tree") || collision.gameObject.layer == LayerMask.NameToLayer("Floor") || collision.gameObject.layer == LayerMask.NameToLayer("Object"))
            if (_rb.velocity.magnitude >= 1f || _rb.velocity.magnitude <= -1f)
                Damage(1);
    }
}
