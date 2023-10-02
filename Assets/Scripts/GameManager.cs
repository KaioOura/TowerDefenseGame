using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Listener")]

    [SerializeField]
    private VoidEventChannel OnPlayerEventChannel;

    [SerializeField]
    private BoolEventChannel OnEnterBuildEventChannel;

    // Start is called before the first frame update
    void Start()
    {
        OnPlayerEventChannel.OnEventRaised += ResetLevel;
        OnEnterBuildEventChannel.OnEventRaised += FreezeGame;
    }

    private void OnDestroy()
    {
        OnPlayerEventChannel.OnEventRaised -= ResetLevel;
        OnEnterBuildEventChannel.OnEventRaised -= FreezeGame;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FreezeGame(bool value)
    {
        if (value)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

}
