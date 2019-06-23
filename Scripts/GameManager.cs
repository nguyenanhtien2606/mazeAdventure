using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Gamekit3D;

public class GameManager : MonoBehaviour 
{

    public maze mazePrefab;
    private maze mazeInstance;
    
	// Use this for initialization
	private void Start () 
    {
        BeginGame();
	}
	
	// Update is called once per frame
	private void Update () 
    {
		if(Input.GetKeyDown(KeyCode.P))
        {
            RestartGame();
        }
	}


    private void BeginGame()
    {
        mazeInstance = Instantiate(mazePrefab) as maze;
        StartCoroutine(mazeInstance.Generate());
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);

        BeginGame();
    }
    
}
