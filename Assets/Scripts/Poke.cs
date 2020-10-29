using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poke : MonoBehaviour
{
    public float points;
    private GameObject _mng;
    private GameManager _manager;

    // Start is called before the first frame update
    void Start()
    {
        _mng = GameObject.Find("GameManager");
        _manager = _mng.GetComponent<GameManager>();
    }


    public void Damage(float damage)
    {
        _manager.AddPoints(points);
        Destroy(gameObject);
    }
}
