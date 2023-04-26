using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineboyController : MonoBehaviour
{
    //PlayerInput is handeld here

    public string inputAxis = "Horizontal";
    public string jumpButton = "Jump";

    public SpineboyModel model;

    private void Update()
    {
        float currentHorizontal = Input.GetAxis(inputAxis);
        model.TryMove(currentHorizontal);

        if (Input.GetKeyDown(KeyCode.W)) {
            model.TryJump();
        }

        if (Input.GetButtonDown(jumpButton)) {
            model.TryJump();
        }
    }
}
