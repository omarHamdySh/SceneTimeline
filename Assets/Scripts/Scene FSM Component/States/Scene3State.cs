using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3State : ISceneState
{
    public SceneStatesFSM FSM;
    public SceneMappingManager.SceneName sceneName = SceneMappingManager.SceneName.Scene3;


    public SceneMappingManager.SceneName GetState()
    {
        return sceneName;
    }

    public void OnStateEnter()
    {
        FSM.manageTimelineUI(true, false, true, true, true);
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
    }
}
