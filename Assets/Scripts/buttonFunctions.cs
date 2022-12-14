using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/*This file creates the functions for all the buttons used within
the menus. Some of the functions have been commented out to preserve
the functionality of the code while other pieces are being uploaded:
-line 24
-line 30
-line 36
-line 47
-line 52
-line 57
*/
public class buttonFunctions : MonoBehaviour
{

    [SerializeField] GameObject buttonSound;
    [SerializeField] GameObject options;

    //ui navigation
    public GameObject optionsFirstButton;
    public GameObject optionsClosedButton;

    //for button that resumes the game
    public void resume()
    {
        if (gameManager.instance.isPaused)
        {
            gameManager.instance.isPaused = !gameManager.instance.isPaused;
            gameManager.instance.CursorUnlockUnpause();
        }
    }

    public void restart()
    {
        Instantiate(buttonSound);
        gameManager.instance.CursorUnlockUnpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void playerRespawn()
    {
        gameManager.instance.playerController.respawn();
        gameManager.instance.isPaused = false;
    }

    public void nextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(next);
    }

    public void showOptions()
    {
        gameManager.instance.optionsOpen = true;
        options.GetComponent<AudioSource>().Play();
        Instantiate(buttonSound);
        options.SetActive(true);

        //setup navigation for ui
        //wipe first
        EventSystem.current.SetSelectedGameObject(null);
        //then set
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void closeOptions()
    {
        gameManager.instance.optionsOpen = false;
        options.GetComponent<AudioSource>().Stop();
        Instantiate(buttonSound);
        options.SetActive(false);

        //setup navigation for ui
        //wipe first
        EventSystem.current.SetSelectedGameObject(null);
        //then set
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }


    public void returnToMain()
    {
        Instantiate(buttonSound);
        SceneManager.LoadScene(0);
    }


    //button to give player HP
    public void giveHP(int amount)
    {
        gameManager.instance.playerController.giveHP(amount);
    }

    public void giveJump(int amount)
    {
        gameManager.instance.playerController.giveJump(amount);
    }

    public void giveSpeed(int amount)
    {
        gameManager.instance.playerController.giveSpeed(amount);
    }
}
