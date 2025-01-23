using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShot : MonoBehaviour
{
    IEnumerator ChangeFunctionAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Change the target function
        PlayerMovement = ModifiedFunction;

        Debug.Log("Function has been changed!");

        // Invoke the target function to show the changed behavior
        targetFunction.Invoke();
    }
}





}
