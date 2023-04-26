using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineboyView : MonoBehaviour
{
    public SpineboyModel model;
    public SkeletonAnimation skeletonAnimation;

    public AnimationReferenceAsset run, idle, jump;

    SpineboyModel.SpineBodyState previousState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((skeletonAnimation.Skeleton.ScaleX < 0) != model.facingLeft) {
            Turn(model.facingLeft);
        }

        SpineboyModel.SpineBodyState currentState = model.state;

        if (previousState != currentState) {
            PlayNewAnimation();
        }

        previousState = currentState;
    }

    void PlayNewAnimation() {
        SpineboyModel.SpineBodyState newState = model.state;
        Spine.Animation nextAnimation;

        if (newState == SpineboyModel.SpineBodyState.Jumping)
        {
            nextAnimation = jump;
        }
        else {
            if (newState == SpineboyModel.SpineBodyState.Running)
            {
                nextAnimation = run;
            }
            else
            {
                nextAnimation = idle;
            }
        }

        skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, true);
    }

    public void Turn(bool facingLeft) {
        skeletonAnimation.Skeleton.ScaleX = facingLeft ? -1f : 1f;
    }
}
