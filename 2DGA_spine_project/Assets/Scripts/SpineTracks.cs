using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineTracks : MonoBehaviour
{

    [SpineAnimation]
    public string walkAnimation;

    [SpineAnimation]
    public string gunGrabAnimation;

    [SpineAnimation]
    public string gunHolsterAnimation;

    SkeletonAnimation skeletonAnimation;

    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;

    void Start() {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;

        StartCoroutine(AnimationDemo());
    }

    IEnumerator AnimationDemo() {
        spineAnimationState.SetAnimation(0, walkAnimation, true);

        while(true) {
            yield return new WaitForSeconds(1.5f);
            spineAnimationState.SetAnimation(1, gunGrabAnimation, false);
            yield return new WaitForSeconds(1.5f);
            spineAnimationState.SetAnimation(1, gunHolsterAnimation, false);
        }
    }
}
