using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineBasicCommands : MonoBehaviour
{
    [SpineAnimation]
    public string walkAnimation;

    [SpineAnimation]
    public string runAnimation;

    [SpineAnimation]
    public string idleAnimation;

    [SpineAnimation]
    public string idleTurnAroundAnimation;

    SkeletonAnimation skeletonAnimation;

    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;

        StartCoroutine(AnimationDemo());
    }

    IEnumerator AnimationDemo() {
        while(true)
        {
            TrackEntry entry = spineAnimationState.SetAnimation(0, walkAnimation, true);
            entry.MixDuration = 0.5f;
            yield return new WaitForSeconds(2f);

            spineAnimationState.SetAnimation(0, runAnimation, true);
            yield return new WaitForSeconds(1.5f);

            spineAnimationState.SetAnimation(0, idleAnimation, true);
            yield return new WaitForSeconds(1.5f);

            skeleton.ScaleX *= -1;
            spineAnimationState.SetAnimation(0, idleTurnAroundAnimation, false);
            spineAnimationState.AddAnimation(0, idleAnimation, true, 0);
            yield return new WaitForSeconds(1f);
        }
    }
}
