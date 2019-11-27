using UnityEngine;
using System.Collections;

public class RotateArround : MonoBehaviour {

	public Transform target; // The object to rotate around
	public float speed; // The speed of rotation
	
	void Start() {
		if (target == null) 
		{
			target = this.gameObject.transform;
			Debug.Log ("RotateAround target not specified. Defaulting to parent GameObject");
		}
	}

	// Update is called once per frame
	void Update () {
        // RotateAround takes three arguments, first is the Vector to rotate around
        // second is a vector that axis to rotate around
        // third is the degrees to rotate, in this case the speed per second
        if (target)
        {
            transform.RotateAround(target.transform.position, target.transform.up, speed * Time.deltaTime);
        }
    }
}
