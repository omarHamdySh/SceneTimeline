using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneStatesFSM : MonoBehaviour
{
    /// <summary>
    /// Declaration of dynamic variables for surving the logic goes here.
    /// </summary>
    //define the stack which controlling the current state

    ISceneState tempFromPause;
    ISceneState currentState;


    // Update is called once per frame
    void Update()
    {
        if (currentState!= null)
        {
            currentState.OnStateUpdate();
        }
    }

    /// <summary>
    /// functions to define the stak functionality
    /// </summary>
    public void PopState()
    {
        if(currentState !=null)
            currentState.OnStateExit();
    }
    public void PushState(ISceneState newState)
    {
        currentState = newState;
        currentState.OnStateEnter();

    }

    public void changeStateTo(ISceneState state) {
        PopState();
        PushState(state);
    }

    public void pauseGame()
    {
        if (tempFromPause == null)
        {
            tempFromPause = currentState;
            PopState();
            //PushState(pauseState);
        }

    }
    public void resumeGame()
    {
        if (tempFromPause != null)
        {
            PopState();
            PushState(tempFromPause);
            tempFromPause = null;
        }
    }

    //return the current state at the stack
    public SceneMappingManager.SceneName getCurrentState()
    {
        return currentState.GetState();
    }
    
    /// <summary>
    /// Dynamically switch timeline scenes switchers' buttons enabled/disabled depending on different scinarios
    /// <br/> 
    /// sx => scene x Timeline Btn (s1 => scene1 Timeline Btn)
    /// </summary>
    /// <param name="s1">Scene 1 Timeline button</param>
    /// <param name="s2">Scene 2 Timeline button</param>
    /// <param name="s3">Scene 3 Timeline button</param>
    /// <param name="s4">Scene 4 Timeline button</param>
    /// <param name="s5">Scene 5 Timeline button</param>
    public void manageTimelineUI(bool s1, bool s2, bool s3, bool s4, bool s5)
    {
        SceneMappingNode[] sceneMappingNodes = GameObject.FindObjectsOfType<SceneMappingNode>();

        foreach (SceneMappingNode smn in sceneMappingNodes)
        {
            switch (smn.sceneName)
            {
                case SceneMappingManager.SceneName.Scene1:
                    smn.GetComponent<Button>().enabled = s1;
                    break;
                case SceneMappingManager.SceneName.Scene2:
                    smn.GetComponent<Button>().enabled = s2;
                    break;
                case SceneMappingManager.SceneName.Scene3:
                    smn.GetComponent<Button>().enabled = s3;
                    break;
                case SceneMappingManager.SceneName.Scene4:
                    smn.GetComponent<Button>().enabled = s4;
                    break;
                case SceneMappingManager.SceneName.Scene5:
                    smn.GetComponent<Button>().enabled = s5;
                    break;
                default:
                    break;
            }
        }
    }
}
