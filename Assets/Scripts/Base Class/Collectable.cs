using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{
    public void CollectThis(GameObject gameObject)
    {
        Destroy(gameObject);
    }
    public virtual void AddToPlayer(CharacterBase characterBase)
    {

    }
}
