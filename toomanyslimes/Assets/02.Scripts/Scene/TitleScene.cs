using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    Button _sceneChangeEventButton;

    void Start()
    {
        StartScene();
    }
    public void StartScene()
    {
        InitReference();
        StartCoroutine(InitManager());
    }

    #region Init Method
    void InitReference()
    {
        DontDestroyOnLoad(this);

        _sceneChangeEventButton = transform.Find("StartButton").GetComponent<Button>();
        _sceneChangeEventButton.onClick.AddListener(OnClickSceneChange);
        _sceneChangeEventButton.gameObject.SetActive(false);
    }
    IEnumerator InitManager()
    {
        LocalizingManager.Instance.Initialize();
        while (LocalizingManager.Instance.IsLoad == false)
            yield return null;

        DataManager.Instance.Initialize();
        while (DataManager.Instance.IsLoad == false)
            yield return null;

        BackgroundManager.Instance.Initialize();
        while (BackgroundManager.Instance.IsLoad == false)
            yield return null;

        SpawnManager.Instance.Initialize();
        while (SpawnManager.Instance.IsLoad == false)
            yield return null;

        StageManager.Instance.Initialize();
        while (StageManager.Instance.IsLoad == false)
            yield return null;

        UIManager.Instance.Initialize();
        while (UIManager.Instance.IsLoad == false)
            yield return null;

        yield return null;
        _sceneChangeEventButton.gameObject.SetActive(true);
    }
    #endregion

    #region Scene Change Method
    void OnClickSceneChange()
    {
        _sceneChangeEventButton.gameObject.SetActive(false);
        StartCoroutine(IESceneChange());
    }
    IEnumerator IESceneChange()
    {
        AsyncOperation asyncOperation = null; 
        asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("GameScene");
        while (asyncOperation == null || !asyncOperation.isDone)
            yield return null;

        Debug.Log("¾À ÀÌµ¿ ¿Ï·á");
        //UIManager.Instance.GameUI.Enable();
        StageManager.Instance.SetupStage(eContentsType.Normal, 1);

        StopAllCoroutines();
        DestroyImmediate(gameObject);
    }
    #endregion
}
