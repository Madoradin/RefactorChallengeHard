using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    private static readonly int CollectableCollectedHash = Animator.StringToHash("CollectableCollected");

    [SerializeField] private Animator uiAnimator;


    public void animatorUpdate()
    {
        uiAnimator.SetTrigger(CollectableCollectedHash);
    }
}
