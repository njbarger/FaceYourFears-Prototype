using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    //public keyPickUp key;
    //public int keyCount;

    public GameObject player;
    public playerController playerController;
    public GameObject playerSpawnPosition;
    public GameObject houseTarget;

    public GameObject currentMenu;

    public GameObject reticle;

    public GameObject pauseMenu;
    public GameObject playerDamage;
    public GameObject levelWinMenu;
    public GameObject gameWinMenu;
    [SerializeField] GameObject playerRespawnMenu;
    public GameObject playerDeadMenu;    
    public GameObject houseDestroyedMenu;
    public GameObject instructions;

    // public GameObject keyIcon;
    //public TextMeshProUGUI keyCountText;
    public GameObject pickUp;
    public GameObject levelMusic;
    //public GameObject menuMusic;

    [Header("------ House Attributes -----")]
    public float houseCurrentHP;
    public float houseMinHP;
    public float houseMaxHP;

    public Image HPBar;
    public Image houseHPBar;
    public TextMeshProUGUI remainingEnemiesLabel;
    public Image SpeedBoostBar;
    public Image DamageBoostBar;
    public int enemyCount;
    public int instructTime;

    public bool isPaused;
    public bool pickedUp;
    public bool optionsOpen;

    float timeScaleOrig;
    public int currentScene;

    //ui navigation
    public GameObject pauseFirstButton;
    public GameObject pRespawnFirstButton;
    public GameObject pDeadFirstButton;
    public GameObject hDeadFirstButton;
    public GameObject winFirstButton;
    public GameObject optionsFirstButton;
    public GameObject optionsClosedButton;



    void Awake()
    {
        Time.timeScale = 1;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        instance = this;
        StartCoroutine(showInstructions());

        player = GameObject.FindGameObjectWithTag("Player");
        houseTarget = GameObject.FindGameObjectWithTag("Base");

        playerController = player.GetComponent<playerController>();
        playerSpawnPosition = GameObject.Find("Player Spawn Position");

        timeScaleOrig = Time.timeScale;
        //key = FindObjectOfType<keyPickUp>();
        isPaused = false;
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {            
        //keyCountText.text = keyCount.ToString();

        if (Input.GetButtonDown("Cancel")  && currentMenu != playerDeadMenu && currentMenu != levelWinMenu && currentMenu != gameWinMenu && optionsOpen == false)
        {
           
            isPaused = !isPaused;
            currentMenu = pauseMenu;
            currentMenu.SetActive(isPaused);
            if (isPaused)
                CursorLockPause();
            else
                CursorUnlockUnpause();
        }

        if(Input.GetButtonDown("Cancel") && optionsOpen)
        {
            buttonFunctions bf = GameObject.FindObjectOfType<buttonFunctions>();
            bf.closeOptions();
        }
    }

    public void CursorLockPause()
    {
        playerDamage.SetActive(false);
        levelMusic.GetComponent<AudioSource>().Pause();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        reticle.SetActive(false);
        Time.timeScale = 0;

        //setup navigation for ui
        //wipe first
        EventSystem.current.SetSelectedGameObject(null);
        //then set
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);


    }
    public void CursorUnlockUnpause()
    {
        levelMusic.GetComponent<AudioSource>().Play();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = timeScaleOrig;
        if (currentMenu != null)
        {
            currentMenu.SetActive(false);
            reticle.SetActive(true);
        }
        currentMenu = null;
    }

    public void showOptions()
    {


        //setup navigation for ui
        //wipe first
        EventSystem.current.SetSelectedGameObject(null);
        //then set
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void closeOptions()
    {


        //setup navigation for ui
        //wipe first
        EventSystem.current.SetSelectedGameObject(null);
        //then set
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

    public void PlayerCanRespawn()
    {
        isPaused = true;
        playerRespawnMenu.SetActive(true);
        currentMenu = playerRespawnMenu;
        CursorLockPause();

        //setup navigation for ui
        //wipe first
        EventSystem.current.SetSelectedGameObject(null);
        //then set
        EventSystem.current.SetSelectedGameObject(pRespawnFirstButton);
    }
    public void PlayerIsDead()
    {
        isPaused = true;
        playerDeadMenu.SetActive(true);
        currentMenu = playerDeadMenu;
        CursorLockPause();

        //setup navigation for ui
        //wipe first
        EventSystem.current.SetSelectedGameObject(null);
        //then set
        EventSystem.current.SetSelectedGameObject(pDeadFirstButton);
    }

    public void houseIsDestroyed()
    {
        isPaused = true;
        houseDestroyedMenu.SetActive(true);
        currentMenu = playerDeadMenu;
        CursorLockPause();

        //setup navigation for ui
        //wipe first
        EventSystem.current.SetSelectedGameObject(null);
        //then set
        EventSystem.current.SetSelectedGameObject(hDeadFirstButton);
    }

    public void updateHouseHP()
    {
        gameManager.instance.houseHPBar.fillAmount = (float)houseCurrentHP / (float)houseMaxHP;
    }

    public void enemyDecrement()
    {
        enemyCount--;
        remainingEnemiesLabel.text = enemyCount.ToString("F0");
        StartCoroutine(CheckEnemyCount());
    }
        public void enemyDecrement(int amount)
    {
        enemyCount -= amount;
        remainingEnemiesLabel.text = enemyCount.ToString("F0");
        StartCoroutine(CheckEnemyCount());
    }

    public void enemyIncrement(int amount)
    {
        enemyCount += amount;
        remainingEnemiesLabel.text = enemyCount.ToString("F0");
    }

    IEnumerator CheckEnemyCount()
    {
        if (enemyCount <= 0)
        {
            yield return new WaitForSeconds(1);

            if(currentScene != 10)
            {
                currentMenu = levelWinMenu;
            }
            else
            {
                currentMenu = gameWinMenu;
            }
            
            isPaused = true;
            currentMenu.SetActive(true);
            CursorLockPause();
        }
    }

    IEnumerator showInstructions()
    {
        instructions.SetActive(true);
        yield return new WaitForSeconds(instructTime);
        instructions.SetActive(false);
    }

    //public void playerGetKey( )
    //{

    //    if (pickedUp)
    //    {
    //        keyCountText.text = keyCount.ToString();

    //        pickedUp = false;
            
    //    }
    //}

    //public void playerLeftKey()
    //{
    //    pickUp.SetActive(false);
    //}

}
