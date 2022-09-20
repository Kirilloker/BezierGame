using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    [SerializeField]
    GameObject go;

    public SceneManager sceneManager;

    public float test = 0.05f;
    public int test2 = 300;

    public bool dwn = false;

    private void Start()
    {
        Switch();
    }
    public void Switch()
    {
        dwn = !dwn;

        if (dwn == false) Up();
        else Down();
    }
    public void Up()
    {
        StartCoroutine(UpCour());   
    }

    private IEnumerator UpCour()
    {
        
        for (int i = 0; i < test2; i++)
        {
            go.transform.position = new Vector2(go.transform.position.x + test, go.transform.position.y+ test);
            yield return new WaitForSeconds(0.01f);
        }

        sceneManager.GoToMainMenuScene();
    }

    public void Down()
    {
        StartCoroutine(DownCour());
    }

    private IEnumerator DownCour()
    {

        for (int i = 0; i < test2; i++)
        {
            go.transform.position = new Vector2(go.transform.position.x - test, go.transform.position.y - test);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
