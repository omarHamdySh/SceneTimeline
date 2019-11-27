using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance)
        {
            GameObject selectedSphere = GameManager.Instance.selectedSphere;
            
            GameObject newObj = Instantiate(
                 GameManager.Instance.CubePrefab,
                 new Vector3(
                     selectedSphere.transform.position.x - (selectedSphere.transform.localScale.x * 2),
                     selectedSphere.transform.position.y,
                     selectedSphere.transform.position.z),
                    Quaternion.identity, this.transform);

            newObj.transform.localScale = new Vector3(
                selectedSphere.transform.localScale.x / 2,
               selectedSphere.transform.localScale.x / 2,
               selectedSphere.transform.localScale.x / 2);
            
            GameManager.Instance.rotateArrountSelectedSphere(newObj);
        }
    }
}
