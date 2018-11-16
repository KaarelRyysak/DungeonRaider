using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Consumable
{
    void Use();
    void Aim();
    void Highlight();
    void Unlight();
    void PickUp();
    Sprite GetSprite();
}