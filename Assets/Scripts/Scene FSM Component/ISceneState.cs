using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneState
{
    void OnStateEnter();
    void OnStateUpdate();
    void OnStateExit();
    string ToString();
    SceneMappingManager.SceneName GetState();
}
