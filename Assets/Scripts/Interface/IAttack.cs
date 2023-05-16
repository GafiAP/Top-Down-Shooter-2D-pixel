using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void SetTarget(Transform target);
    Transform GetTarget();
    void SetCoolDown(float value);
    float GetCoolDown();
}
