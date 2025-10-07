using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneUI : MonoBehaviour
{
    public void LoadS(string Scene) {
        SceneManager.LoadScene(Scene);
    }
}
