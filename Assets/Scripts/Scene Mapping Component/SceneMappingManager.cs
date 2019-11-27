#region Preprocessors directives

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#endregion
public class SceneMappingManager : MonoBehaviour
{

    #region Scene Mapping Manager Attributes/Data Memebers

    private static SceneMappingManager _Instance;                          //Singleton data member

    #region Scenes Finite State Machine Data Memebers

    private SceneStatesFSM sceneStatesFSM;
    private Scene1State scene1State;
    private Scene2State scene2State;
    private Scene3State scene3State;
    private Scene4State scene4State;
    private Scene5State scene5State;
    
    #endregion
    
    /// <summary>
    /// The name of the enum is the exact same name of the scene that will be mapped to.
    /// you can use this enum anywhere to know through code easily at what scene you are at the moment.
    /// </summary>
    public enum SceneName
    {
        Scene1,
        Scene2,
        Scene3,
        Scene4,
        Scene5
    }

    [HideInInspector]
    public SceneName currentScene;                                         //Variable to hold the current scene to be shared.

    public Image fadeImage;                                                //The blank image to be used in the fading effect.

    [SerializeField]
    private GameObject restartBtn;                                         //The restart button that needed to be enabled at the last scene of the timeline.

    private static SceneMappingNode[] sceneMappingNodes;                   //Scene mapping nodes/ Buttons that will be used to load scenes.                    
    public static SceneMappingManager Instance                             //Getter Method.
    {
        get { return _Instance; }

    }

    #endregion

    #region Scene Mapping Manager Methods/Functions

    /// <summary>
    /// Singleton Code then:
    ///     Subscribing for OnSceneLoaded Event in order to fire methods when the game starts/ the scenes loaded.
    /// </summary>
    private void Awake()
    {
        if (_Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _Instance = this;
        }
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded; //Subscribe for Scene Loaded Event
    }
    private void Start()
    {
        sceneStatesFSM = GetComponent<SceneStatesFSM>();

        scene1State = new Scene1State(){
            FSM = sceneStatesFSM
        }; 

        scene2State = new Scene2State() {
            FSM = sceneStatesFSM
        }; 

        scene3State = new Scene3State(){
            FSM = sceneStatesFSM
        };

        scene4State = new Scene4State(){
            FSM = sceneStatesFSM
        }; 

        scene5State = new Scene5State(){
            FSM = sceneStatesFSM
        };

        sceneStatesFSM.changeStateTo(scene1State);

    }
    
    #region On Scene Loaded Methods, Events, Actions Handeling

    /// <summary>
    /// Each time On Scene Loaded the next method will be invoked.
    /// Start the Fade In transition animation.
    /// Set the default scene timeline button to be selected/pressed. 
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (fadeImage)
            fadeImage.GetComponent<Animator>().SetTrigger("FadeIn");

    }


    #endregion


    #region Scene Mapping, Changing, Fading Methods

    /// <summary>
    /// This Method is made to be used from inside the code not from the inspector since it doesn't take a primitive datatype
    /// as a paramter, instead it takes enum which will make the method unable to appear at any unity event in inspector.
    /// </summary>
    /// <param name="sceneName"></param>
    public void changeScene(SceneName sceneName)
    {
        currentScene = sceneName;
        SceneManager.LoadScene(sceneName.ToString());
        ChangeSceneState(sceneName);
    }

    /// <summary>
    /// Map and change the current sceneState to the recent loaded scene.
    /// </summary>
    /// <param name="sceneName"></param>
    private void ChangeSceneState(SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.Scene1:
                sceneStatesFSM.changeStateTo(scene1State);
                break;
            case SceneName.Scene2:
                sceneStatesFSM.changeStateTo(scene2State);
                break;
            case SceneName.Scene3:
                if (restartBtn)
                    restartBtn.SetActive(true);
                sceneStatesFSM.changeStateTo(scene3State);
                break;
            case SceneName.Scene4:
                sceneStatesFSM.changeStateTo(scene4State);
                break;
            case SceneName.Scene5:
                sceneStatesFSM.changeStateTo(scene5State);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sceneName"></param>
    public void fadeOutAndChangeScene(SceneName sceneName)
    {
        StartCoroutine(fadeoutScene(sceneName));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator fadeoutScene(SceneName sceneName)
    {
        playFadingAnimation();
        yield return new WaitUntil(isFadeAnimationDone);
        yield return new WaitForSeconds(0.15f);
        changeScene(sceneName);
    }

    /// <summary>
    ///Check whether the animation of the fading is done or not yet.
    ///The Co routine is using the method waiting for the condition to be done in order to
    ///Complete the transition form the scene to another.
    /// </summary>
    /// <returns></returns>
    public bool isFadeAnimationDone()
    {
        if (fadeImage.color.a >= 0.8f)
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    ///Plays the Animation of the fading out using animator component.
    /// </summary>
    public void playFadingAnimation()
    {
        fadeImage.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    #endregion

    #endregion

}
