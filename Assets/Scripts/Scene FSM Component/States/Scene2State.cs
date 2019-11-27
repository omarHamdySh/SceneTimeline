using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2State : ISceneState
{
    public SceneStatesFSM FSM;
    public SceneMappingManager.SceneName sceneName = SceneMappingManager.SceneName.Scene2;

    public SceneMappingManager.SceneName GetState()
    {
        return sceneName;
    }

    public void OnStateEnter()
    {
        FSM.manageTimelineUI(true, true, false, true, true);
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
    }
}
