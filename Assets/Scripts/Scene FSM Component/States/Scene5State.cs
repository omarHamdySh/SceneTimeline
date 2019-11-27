using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene5State : ISceneState
{
    public SceneStatesFSM FSM;
    public SceneMappingManager.SceneName sceneName = SceneMappingManager.SceneName.Scene5;

    public SceneMappingManager.SceneName GetState()
    {
        return sceneName;
    }

    public void OnStateEnter()
    {
        FSM.manageTimelineUI(true, false, false, true, true);
        GameObject.Destroy(GameManager.Instance.selectedSphere);
        GameObject.Destroy(GameManager.Instance.cubeInstance);
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
    }


}
