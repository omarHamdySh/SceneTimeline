using UnityEngine;
using System.Collections;

public class ChangeLookAtTarget : MonoBehaviour
{

    public GameObject target; // the target that the camera should look at
    private float maxFOV;

    void Start()
    {
        if (target == null)
        {
            target = this.gameObject;
        }
    }

    // Called when MouseDown on this gameObject
    void OnMouseDown()
    {
        // change the field of view on the perspective camera based on the distance from center of world, clamp it to a reasonable field of view
        maxFOV = Mathf.Clamp(15 * target.transform.localScale.x, 1, 100);

        //Start the transition from scene to scene and carry the selected sphere to the next scene.
        GameManager.Instance.startTransition(this.gameObject);
    }


}
