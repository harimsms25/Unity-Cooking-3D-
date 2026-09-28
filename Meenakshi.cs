using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public TMP_Text numberOfLives;
    public TMP_Text finalScore;
    public GameObject gameManager;
    public GameObject gameOverPanel;
    private bool plateInHand;
    public Image crossHairInGame;
    private float sizeOfCross = MenuManager.corsshairSetInMenu;
    private int lives;
    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }
    public bool PlateInHand
    {
        get { return plateInHand; }
        set { plateInHand = value; }
    }

    public GameObject rightArm;
    private int points;

    public int Points
    {
        get { return points; }
        set { points = value; }
    }
    public GameObject Dish;
    public GameObject dishSpawnPoint;
    public Material highlightColor;
    private Transform highlight;
    private Material originalMaterial;
    private GameObject lastHighlighted;
    private GameObject objectOnPlate;
    public GameObject ObjectOnPlate
    {
        get { return objectOnPlate; }
        set { objectOnPlate = value; }
    }
    private string order;

    public string Order
    {
        get { return order; }
        set { order = value; }
    }
    private bool enabledHighlighting;

    public bool EnabledHighlighting
    {
        get { return enabledHighlighting; }
        set { enabledHighlighting = value; }
    }
    public GameObject CurrentPlate
    {
        get { return currentPlate; }
        set { CurrentPlate = value; }
    }
    private bool pattyInHand;
    public bool PattyInHand
    {
        get { return pattyInHand; }
        set { pattyInHand = value; }
    }

    public GameObject foodSpawn;
    public GameObject BurgerBuns;
    private GameObject currentPlate;
    public GameObject EverythingButPatty;
    public GameObject Lettuce_Cheese;
    public GameObject Tomatoes_Lettuce;
    public GameObject Tomatoes_Cheese;
    public GameObject Lettuce_Buns;
    public GameObject Cheese_Buns;
    public GameObject Tomatoes_Buns;
    public GameObject AllTopings;
    public GameObject LettuceCheese;
    public GameObject TomatoesLettuce;
    public GameObject TomatoesCheese;
    public GameObject Lettuce;
    public GameObject Cheese;
    public GameObject Tomatoes;
    public GameObject RawPatty;
    public GameObject notDestroyable;
    public GameObject notDestroyable2;
    public GameObject _1;
    public GameObject _2;
    public GameObject _3;
    public GameObject _4;
    public GameObject _5;
    public GameObject _6;
    public GameObject _7;
    public GameObject _8;

    public GameObject x2;
    public GameObject x3;
    public GameObject x4;
    public GameObject x5;
    public GameObject x6;
    public GameObject x7;
    public GameObject x8;
    public GameObject cookedPatty;
    public GameObject directionsNote;
    private bool gameStarted;

    public GameObject dropperOne;
    public GameObject dropperTwo;
    public GameObject dropperThree;
    private DropperScripts d1;
    private DropperScripts d2;
    private DropperScripts d3;
    private bool hasBurntPatty;

    public bool HasBurntPatty
    {
        get { return hasBurntPatty; }
        set { hasBurntPatty = value; }
    }
    public bool GameStarted
    {
        get { return gameStarted; }
        set { gameStarted = value; }
    }



    // Start is called before the first frame update
    void Start()
    {
        Lives = 3;
        PlateInHand = false;
        PattyInHand = false;
        order = "";
        enabledHighlighting = false;
        objectOnPlate = notDestroyable;
        currentPlate = notDestroyable2;
        objectOnPlate.tag = "Null";
        currentPlate.tag = "Null";
        GameStarted = false;
        d1 = dropperOne.GetComponent<DropperScripts>();
        d2 = dropperTwo.GetComponent<DropperScripts>();
        d3 = dropperThree.GetComponent<DropperScripts>();
        Points = 0;
        crossHairInGame.transform.localScale = new Vector3(sizeOfCross, sizeOfCross, 1);
        print(crossHairInGame.transform.localScale + "");
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        //print(Time.timeScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (Lives > 0)
        {
            raycastingCheck();
            numberOfLives.text = "Lives: " + Lives;
        }
        if (Lives == 0)
        {
            numberOfLives.text = "Lives: " + Lives;
            GameOverFunction();
        }
        print(ObjectOnPlate.tag);
        // if (enabledHighlighting)
        // {
        //     highlightingMethod();
        // }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    void raycastingCheck()
    {

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //print(hit.transform.gameObject.tag);
                if (hit.transform.gameObject.CompareTag("Directions"))
                {
                    Destroy(directionsNote);
                    GameStarted = true;
                    d1.PlayOn = true;
                    d2.PlayOn = true;
                    d3.PlayOn = true;
                }
                if (GameStarted)
                {
                    if (hit.transform.gameObject.CompareTag("DishRack") && !PlateInHand && !PattyInHand && !HasBurntPatty)
                    {
                        Quaternion rotationOfArm = Quaternion.Euler(new Vector3(0, -120, 90));
                        rightArm.transform.localRotation = rotationOfArm;
                        currentPlate = Instantiate(Dish, dishSpawnPoint.transform.position, Quaternion.identity, this.transform);
                        PlateInHand = true;
                    }
                    if (hit.transform.gameObject.CompareTag("RawPatty") && !PlateInHand && !PattyInHand && !HasBurntPatty)
                    {
                        Quaternion rotationOfArm = Quaternion.Euler(new Vector3(0, -120, 90));
                        rightArm.transform.localRotation = rotationOfArm;
                        objectOnPlate = Instantiate(RawPatty, dishSpawnPoint.transform.position, Quaternion.identity, this.transform);
                        PattyInHand = true;
                    }
                    if (hit.transform.gameObject.CompareTag("BurgerBuns") && PlateInHand && !(order.IndexOf("BurgerBuns") >= 0))
                    {
                        if (!objectOnPlate.CompareTag("Null"))
                        {
                            Destroy(objectOnPlate);
                        }
                        order += "BurgerBuns";
                        objectOnPlate = Instantiate(checkingWhatObjectMethod(), foodSpawn.transform.position, Quaternion.identity, this.transform);
                        if (order.Equals("BurgerBuns"))
                        {
                            objectOnPlate.transform.localScale = new Vector3(15f, 15f, 15f);
                        }
                        else
                        {
                            objectOnPlate.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

                        }
                    }
                    if (hit.transform.gameObject.CompareTag("Tomatoes") && PlateInHand && !(order.IndexOf("Tomatoes") >= 0))
                    {
                        if (!objectOnPlate.CompareTag("Null"))
                        {
                            Destroy(objectOnPlate);
                        }
                        order += "Tomatoes";
                        objectOnPlate = Instantiate(checkingWhatObjectMethod(), foodSpawn.transform.position, Quaternion.identity, this.transform);
                        objectOnPlate.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    }
                    if (hit.transform.gameObject.CompareTag("Lettuce") && PlateInHand && !(order.IndexOf("Lettuce") >= 0))
                    {
                        if (!objectOnPlate.CompareTag("Null"))
                        {
                            Destroy(objectOnPlate);
                        }
                        order += "Lettuce";
                        objectOnPlate = Instantiate(checkingWhatObjectMethod(), foodSpawn.transform.position, Quaternion.identity, this.transform);
                        objectOnPlate.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        if (order.Equals("Lettuce"))
                        {
                            objectOnPlate.transform.localScale = new Vector3(1f, 1f, 1f);
                        }
                        else
                        {
                            objectOnPlate.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

                        }
                    }
                    if (hit.transform.gameObject.CompareTag("Cheese") && PlateInHand && !(order.IndexOf("Cheese") >= 0))
                    {
                        if (!objectOnPlate.CompareTag("Null"))
                        {
                            Destroy(objectOnPlate);
                        }
                        order += "Cheese";
                        objectOnPlate = Instantiate(checkingWhatObjectMethod(), foodSpawn.transform.position, Quaternion.identity, this.transform);
                        if (order.Equals("Cheese"))
                        {
                            objectOnPlate.transform.localScale = new Vector3(1f, 1f, 1f);
                        }
                        else
                        {
                            objectOnPlate.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

                        }

                    }
                    if (hit.transform.gameObject.CompareTag("Trash"))
                    {
                        //objectOnPlate = notDestroyable;
                        if (!objectOnPlate.CompareTag("Null"))
                        {
                            Destroy(objectOnPlate);
                        }
                        if (!currentPlate.CompareTag("Null"))
                        {
                            Destroy(currentPlate);
                        }
                        objectOnPlate = notDestroyable;
                        currentPlate = notDestroyable2;
                        objectOnPlate.tag = "Null";
                        currentPlate.tag = "Null";
                        PlateInHand = false;
                        PattyInHand = false;
                        HasBurntPatty = false;
                        order = "";
                        Quaternion rotationOfArm = Quaternion.Euler(new Vector3(5, -90, 0));
                        rightArm.transform.localRotation = rotationOfArm;
                    }
                }
            }
        }
    }

    public GameObject checkingWhatObjectMethod()
    {
        if (order.IndexOf("Patty") >= 0)
        {
            if (order.IndexOf("BurgerBuns") >= 0)
            {
                if (order.IndexOf("Cheese") >= 0 && order.IndexOf("Tomatoes") >= 0 && order.IndexOf("Lettuce") >= 0)
                {
                    return _2;
                }
                else if (order.IndexOf("Cheese") >= 0 && order.IndexOf("Tomatoes") >= 0)
                {
                    return _5;
                }
                else if (order.IndexOf("Lettuce") >= 0 && order.IndexOf("Tomatoes") >= 0)
                {
                    return _4;
                }
                else if (order.IndexOf("Cheese") >= 0 && order.IndexOf("Lettuce") >= 0)
                {
                    return _3;
                }
                else if (order.IndexOf("Cheese") >= 0)
                {
                    return _6;
                }
                else if (order.IndexOf("Lettuce") >= 0)
                {
                    return _7;
                }
                else if (order.IndexOf("Tomatoes") >= 0)
                {
                    return _8;
                }
                else
                {
                    return _1;
                }
            }
            else
            {
                if (order.IndexOf("Cheese") >= 0 && order.IndexOf("Tomatoes") >= 0 && order.IndexOf("Lettuce") >= 0)
                {
                    return x2;
                }
                else if (order.IndexOf("Cheese") >= 0 && order.IndexOf("Tomatoes") >= 0)
                {
                    return x5;
                }
                else if (order.IndexOf("Lettuce") >= 0 && order.IndexOf("Tomatoes") >= 0)
                {
                    return x4;
                }
                else if (order.IndexOf("Cheese") >= 0 && order.IndexOf("Lettuce") >= 0)
                {
                    return x3;
                }
                else if (order.IndexOf("Cheese") >= 0)
                {
                    return x6;
                }
                else if (order.IndexOf("Lettuce") >= 0)
                {
                    return x7;
                }
                else if (order.IndexOf("Tomatoes") >= 0)
                {
                    return x8;
                }
                else
                {
                    return cookedPatty;
                }
            }
        }
        else
        {
            if (order.IndexOf("BurgerBuns") >= 0)
            {
                if (order.IndexOf("Cheese") >= 0 && order.IndexOf("Tomatoes") >= 0 && order.IndexOf("Lettuce") >= 0)
                {
                    return EverythingButPatty;
                }
                else if (order.IndexOf("Cheese") >= 0 && order.IndexOf("Tomatoes") >= 0)
                {
                    return Tomatoes_Cheese;
                }
                else if (order.IndexOf("Lettuce") >= 0 && order.IndexOf("Tomatoes") >= 0)
                {
                    return Tomatoes_Lettuce;
                }
                else if (order.IndexOf("Cheese") >= 0 && order.IndexOf("Lettuce") >= 0)
                {
                    return Lettuce_Cheese;
                }
                else if (order.IndexOf("Cheese") >= 0)
                {
                    return Cheese_Buns;
                }
                else if (order.IndexOf("Lettuce") >= 0)
                {
                    return Lettuce_Buns;
                }
                else if (order.IndexOf("Tomatoes") >= 0)
                {
                    return Tomatoes_Buns;
                }
                else
                {
                    return BurgerBuns;
                }

            }
            else
            {
                if (order.IndexOf("Cheese") >= 0 && order.IndexOf("Tomatoes") >= 0 && order.IndexOf("Lettuce") >= 0)
                {
                    return AllTopings;
                }
                else if (order.IndexOf("Cheese") >= 0 && order.IndexOf("Tomatoes") >= 0)
                {
                    return TomatoesCheese;
                }
                else if (order.IndexOf("Lettuce") >= 0 && order.IndexOf("Tomatoes") >= 0)
                {
                    return TomatoesLettuce;
                }
                else if (order.IndexOf("Cheese") >= 0 && order.IndexOf("Lettuce") >= 0)
                {
                    return LettuceCheese;
                }
                else if (order.IndexOf("Cheese") >= 0)
                {
                    return Cheese;
                }
                else if (order.IndexOf("Lettuce") >= 0)
                {
                    return Lettuce;
                }
                else if (order.IndexOf("Tomatoes") >= 0)
                {
                    return Tomatoes;
                }
            }
        }
        return null;
    }
    void GameOverFunction()
    {
        Time.timeScale = 0;
        gameManager.SetActive(false);
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        finalScore.text = "" + Points;
    }
    public void quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
