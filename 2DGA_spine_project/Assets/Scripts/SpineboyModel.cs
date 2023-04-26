using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineboyModel : MonoBehaviour
{
    public SpineBodyState state;
    public bool facingLeft;
    public float currentSpeed;

    public void TryJump() {
        StartCoroutine(JumpRoutine());
    }

    IEnumerator JumpRoutine() {

        if (state == SpineBodyState.Jumping) {
            yield break;
        }

        state = SpineBodyState.Jumping;

        Vector3 position = transform.localPosition;
        float jumpTime = 1.2f;
        float half = jumpTime * 0.5f;
        float jumpPower = 20f;

        for (float t = 0; t < half; t += Time.deltaTime) {
            float powerIncrement = jumpPower * (half - t);
            transform.Translate((powerIncrement * Time.deltaTime) * Vector3.up);
            yield return null;
        }
        for (float t = 0; t < half; t += Time.deltaTime) {
            float powerIncrement = jumpPower * t;
            transform.Translate((powerIncrement * Time.deltaTime) * Vector3.down);
            yield return null;
        }
        transform.localPosition = position;

        state = SpineBodyState.Idle;
    }

    public void TryMove(float speed) {
        currentSpeed = speed;

        if (speed != 0)
        {
            bool speedIsNegative = speed < 0f;
            facingLeft = speedIsNegative;
        }

        if (state != SpineBodyState.Jumping) {
            state = (speed == 0) ? SpineBodyState.Idle : SpineBodyState.Running;
        }
    }

    public enum SpineBodyState { 
        Idle,
        Running,
        Jumping
    }
}
