using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Game Manager Data Member
    private static GameManager _Instance;                       //Singleton Data Memeber.

    private ChangeLookAtTarget[] spheres;                       //Array that suppose to hold all the spheres objects when it comes.
   
    public bool isUnSelectedSpheresFaded;                       //A boolean flag which indicates that fading animation is done.

    [HideInInspector]
    public GameObject selectedSphere;                           //A reference to the selectedSphere.

    public GameObject CubePrefab;                               //A cube Prefab to instatiate from.

    public GameObject cubeInstance;                             //A cube reference in order to destroy it later.

    #endregion

    #region Game Manager Methods
    public static GameManager Instance                          //Singletone Method
    {
        get { return _Instance; }
    }

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

        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;              //Subscribe OnSceneLoaded method to the sceneLoaded Event.
    }


    /// <summary>
    /// At SceneLoaded Event time:
    /// If this is the scene 3 which should have the selectedSphere in the middle of the screen
    /// so we are going to reposition the camera using LookAtTarget Script, and changing the FOV to zoom in randomly 
    /// at the selected sphere.
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (selectedSphere)
        {
            if (arg0.name == SceneMappingManager.SceneName.Scene3.ToString())
            {
                Camera.main.fieldOfView = Mathf.Clamp(15 * selectedSphere.transform.localScale.x, 1, 100);
                selectedSphere.transform.position = Vector3.zero; 
                LookAtTarget.target = selectedSphere;
                selectedSphere.GetComponent<ChangeLookAtTarget>().enabled = false;
            }
        }
    }

    /// <summary>
    /// First initialize the selectedSphere with the givin value.
    /// Change the LookAtTarget in order to make the sphere in the middle of the screen for this scene.
    /// Change the FOV in order to zoom in at the selectedSphere.
    /// Make the selectedSphere GameObject DontDestroyOnLoad(selectedSphere);
    /// Fadout the Scene and change to the next scene => Scene3
    /// </summary>
    /// <param name="selectedSphere"></param>
    /// <param name="maxFOV"></param>
    public void startTransition(GameObject selectedSphere)
    {
        this.selectedSphere = selectedSphere;

        fadeOutAndSelectASphere();
        
        DontDestroyOnLoad(selectedSphere);

        StartCoroutine(ProceedAfterAWhile());
    }

    /// <summary>
    /// Waiting until the fading game objects to be fully faded out and disappeared and then change the scene.
    /// </summary>
    /// <returns></returns>
    IEnumerator ProceedAfterAWhile() {
        
        yield return new WaitUntil(isFadeAnimationDone);
        
        isUnSelectedSpheresFaded = false;
        
        SceneMappingManager.Instance.fadeOutAndChangeScene(SceneMappingManager.SceneName.Scene3);
    }

    public bool isFadeAnimationDone()
    {
       return isUnSelectedSpheresFaded;
    }

    /// <summary>
    /// Get all the spheres using the ChangeLookAtTarget Script in order to some operations over them.
    /// Destroy the Rotate arround script of all the spheres.
    /// Make the parent of each equal to null.
    /// Fade out the other spheres but the seleceted one.
    /// </summary>
    /// <param name="selectedSphere"></param>
    private void fadeOutAndSelectASphere()
    {
        spheres = FindObjectsOfType<ChangeLookAtTarget>();

        foreach (var sphere in spheres)
        {
            Destroy(sphere.GetComponent<RotateArround>());
            sphere.transform.parent = null;

            if (selectedSphere != sphere.gameObject)
            {
                sphere.gameObject.GetComponent<Animator>().SetTrigger("FadeOut");
            }
        }
    }

    /// <summary>
    /// Making the created object rotates arround the selected sphere.
    /// </summary>
    /// <param name="newObject"></param>
    public void rotateArrountSelectedSphere(GameObject newObject) {

        cubeInstance = newObject; 

        RotateArround rotateArround = cubeInstance.AddComponent<RotateArround>();

        if (selectedSphere)
            rotateArround.target = selectedSphere.transform;

        rotateArround.speed = 10f;

    }

    #endregion
}
