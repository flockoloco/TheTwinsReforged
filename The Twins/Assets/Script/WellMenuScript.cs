using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TheTwins.Model;

public class WellMenuScript : MonoBehaviour
{
    public GameObject player;
    public GameObject wellMenu;
    public GameObject ChoosingCanvas;
    public GameObject ReceiveCanvas;
    private GameManagerScript gameManager;

    [SerializeField]
    private int[] selectedCurrency;

    [SerializeField]
    private int[] availableCurrency;


    public Button oreButton0;
    public Button oreButton1;
    public Button oreButton2;
    public Button oreButton3;

    public Button barsButton0;
    public Button barsButton1;
    public Button barsButton2;
    public Button barsButton3;

    public TextMeshProUGUI oreText;
    public TextMeshProUGUI barsText;

    public Button submitButton;

    private PlayerStats playerstats;

    public GameObject CanvasChanger;
   

    public GameObject popUpPrefab;

    private void Awake()
    {
        wellMenu.SetActive(false);
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();


        oreButton0.onClick.AddListener(delegate { ChangeValue(-10, 1); });// first digit is how much the selectedcurrency[second digit] will change.
        oreButton1.onClick.AddListener(delegate { ChangeValue(-1, 1); });// second digit, the reference to the type of currency.
        oreButton2.onClick.AddListener(delegate { ChangeValue(1, 1); });
        oreButton3.onClick.AddListener(delegate { ChangeValue(10, 1); });

        barsButton0.onClick.AddListener(delegate { ChangeValue(-10, 0); });
        barsButton1.onClick.AddListener(delegate { ChangeValue(-1, 0); });
        barsButton2.onClick.AddListener(delegate { ChangeValue(1, 0); });
        barsButton3.onClick.AddListener(delegate { ChangeValue(10, 0); });

        submitButton.onClick.AddListener(delegate { Submit(); });

        selectedCurrency = new int[2];
        availableCurrency = new int[2];
    }

    public void Activate()
    {
        wellMenu.SetActive(true);
        CanvasChanger.GetComponent<UIPopUpScript>().CanvasSwitcher(0);

        UpdateAvailableCurrency();
        
        
        selectedCurrency[0] = 0;
        selectedCurrency[1] = 0;
        UpdateText();
    }

    public void ChangeValue(int number, int type)
    {
        int testingResult = selectedCurrency[type] + number;

        if (testingResult < 0)
        {
            testingResult = 0;
        }
        else if (testingResult > availableCurrency[type])
        {
            testingResult = availableCurrency[type];
        }
        selectedCurrency[type] = testingResult;
        testingResult = 0;

        UpdateText();
    }

    private void Submit()
    {
        if ((selectedCurrency[0] > 0) || (selectedCurrency[1] > 0))
        {
            //POST mandar selectedCurrency como um arrayy :D
            gameManager.SendDelivery(selectedCurrency[0],selectedCurrency[1]);


            player.GetComponent<PlayerStats>().bars -= selectedCurrency[0];
            player.GetComponent<PlayerStats>().nuggets -= selectedCurrency[1];

            gameManager.playerCurrency.Bars -= selectedCurrency[0];
            gameManager.playerCurrency.Ores -= selectedCurrency[1];


            UpdateAvailableCurrency();

            selectedCurrency[0] = 0;
            selectedCurrency[1] = 0;

            UpdateText();
        }
        else
        {
            //ask to put something in the inputs
        }
    }

    private void UpdateAvailableCurrency()
    {
        playerstats = player.GetComponent<PlayerStats>();

        availableCurrency[0] = playerstats.bars;
        availableCurrency[1] = playerstats.nuggets;
    }

    private void UpdateText()
    {
        barsText.text = "Selected Bars: " + selectedCurrency[0];
        oreText.text = "Selected Ores: " + selectedCurrency[1];
    }
    public void CheckReceivedResources()
    {
        gameManager.CheckDelivery(0);
    }
    public void AcceptReceivedResources()
    {
        gameManager.CheckDelivery(1);
    }
    public void ReturnReceivedResourcesCheck(DeliveryHolder receivedDelivery)
    {
        int oresToSend = 0;
        int barsToSend = 0;

        if (receivedDelivery.Status == 0)
        {
            oresToSend = receivedDelivery.OresAmount;
            barsToSend = receivedDelivery.BarsAmount;
        }

        CanvasChanger.GetComponent<UIPopUpScript>().CanvasSwitcher(1);
        ReceiveCanvas.GetComponent<TextWellReceiveUpdater>().UpdateText(oresToSend, barsToSend);

    }
    public void ReturnReceivedResourcesAccept()
    {

        CanvasChanger.GetComponent<UIPopUpScript>().CanvasSwitcher(0);

        GameObject popUp = Instantiate(popUpPrefab, ChoosingCanvas.transform);
        popUp.transform.position = new Vector3(popUp.transform.position.x, popUp.transform.position.y - 300, popUp.transform.position.z);
        popUp.GetComponent<DialogScript>().GiveText("Values added!");
    }
}