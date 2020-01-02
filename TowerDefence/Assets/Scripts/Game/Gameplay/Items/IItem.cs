using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Items
{
    public interface IItem
    {
        void Use();
        void ConstEffect();
        void Choose();
        Sprite Image { get; }
        int Id { get; }
        string Name { get; }
        string Desc { get; }
        GameObject ClickedObject { get; set; }
        Vector3 ClickedPosition { get; set; }
    }
}