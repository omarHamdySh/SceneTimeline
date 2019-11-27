using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMappingNode : MonoBehaviour
{
    public SceneMappingManager.SceneName sceneName;             //The Scene Name of this button that when clicked will be changed to.
    public Button thisBtn;                                      //A reference to the button Button Component in order to be used to be highlighted as selected button of a selected scene

    private void Start()
    {
        thisBtn = GetComponent<Button>();
    }
    public void ChangeScene() {
        SceneMappingManager.Instance.fadeOutAndChangeScene(sceneName);
    }
    public void Update()
    {
        //If the current loaded scene is the same scene of this button so highlight this button to be selected.
        if (sceneName == SceneMappingManager.Instance.currentScene)
        {
            thisBtn.Select();
        }
    }
}
