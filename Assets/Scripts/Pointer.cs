using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    private Vector2 _clickPosition;
    private Vector2 _dragPosition;
    private bool _clickDown;
    private bool _gotClickPos;
    private Vector2 _dragDistance;
    private GameObject _activeBall;
    private bool _ballClicked;
    public float shotForce;
    public bool _canClick = true;
    public GameManager manager;
   
    // Update is called once per frame
    void Update()
    {
        if (_canClick)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _clickDown = true;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _clickDown = false;
                _gotClickPos = false;
                if (_ballClicked)
                {
                    ApplyForce();
                    _ballClicked = false;
                }
            }

            if (_clickDown && _gotClickPos == false)
            {
                ClickPosition();
                _gotClickPos = true;
            }

            if (_clickDown)
                PointerDrag();
        } 
    }

    private void ClickPosition()
    {
        _clickPosition = Input.mousePosition;
        CheckObject();
    }

    private void PointerDrag()
    {
        _dragPosition = Input.mousePosition;
        _dragDistance = _dragPosition - _clickPosition;
    }

    private void CheckObject()
    {
        RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(_clickPosition), Vector2.zero);
        if (ray == true && ray.collider.gameObject.layer == LayerMask.NameToLayer("Pokeball"))
        {
            _activeBall = ray.collider.gameObject;
            _ballClicked = true;
            manager.canChange = false;
        }
    }

    private void ApplyForce()
    {
        Rigidbody2D ball = _activeBall.gameObject.GetComponent<Rigidbody2D>();
        ball.AddForce(_dragDistance * -1 * shotForce);
        manager.ReduceQuantity();
        manager.canChange = true;
    }

    public void ClickState(bool state)
    {
        _canClick = state;
        Debug.Log("Can click: " + _canClick);
    }
}
