using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{

    /// <summary>
    /// 
    /// </summary>
    public void restart() {
       
        if(GameManager.Instance.selectedSphere)
            Destroy(GameManager.Instance.selectedSphere, 0.8f);
       
        SceneMappingManager.Instance.fadeOutAndChangeScene(SceneMappingManager.SceneName.Scene1);
    }
}
