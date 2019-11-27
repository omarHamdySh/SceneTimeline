using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutNotifier : MonoBehaviour
{
    public void DeclareFadedOutState() {
        GameManager.Instance.isUnSelectedSpheresFaded = true;
    }
}
