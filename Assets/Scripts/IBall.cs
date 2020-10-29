using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBall
{
    void Ability();

    void Movement();

    void DoDamage(GameObject obj);
}
