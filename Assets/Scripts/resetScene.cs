using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetScene : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    public void OnButtonPress()
    {
        Debug.Log("clic");
        SceneManager.LoadScene("Map");
        Time.timeScale = 1;
    }
}
