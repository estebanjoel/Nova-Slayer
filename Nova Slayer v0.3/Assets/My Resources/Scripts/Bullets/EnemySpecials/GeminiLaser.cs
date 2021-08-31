using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeminiLaser : Bullet
{

    public override void Move()
    {
        power = power + 0.001f;
    }

    public override bool CheckCollision(GameObject target)
    {
        return false;
    }
}

