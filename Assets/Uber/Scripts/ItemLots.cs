
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using TMPro;

public class ItemLots : MonoBehaviour
{
    [Header("Item Pools")]
    public List<SO_BikeItem> firstPool = new List<SO_BikeItem>();
    public List<SO_BikeItem> secondPool = new List<SO_BikeItem>();

    [Header("Spawn")]
    public Transform spawnPoint;

    [Header("Display")]
    public Vector2 spriteSize = new Vector2(180, 180);
    public float horizontalSpacing = 300f;
    public float displayHeight = 0f;

    [Header("Text")]
    public TMP_FontAsset itemFont;
    public int nameFontSize = 28;
    public Color textColor = Color.white;

    [Header("Selection")]
    public Color selectedColor = Color.yellow;
    public Color normalColor = Color.white;

    private List<SO_BikeItem> currentItems = new List<SO_BikeItem>();

    private Canvas canvas;
    private Button[] itemButtons = new Button[3];
    private Image[] itemImages = new Image[3];
    private TMP_Text[] nameTexts = new TMP_Text[3];

    private int selectedIndex = 0;
    private bool menuActive = false;

    private GameObject createdEventSystem;

    private bool previousCursorVisible;
    private CursorLockMode previousCursorLock;

    private float nextNavigationTime;
    public float navigationDelay = 0.2f;

    public MealGestion _mealGestion;

    private void Update()
    {
        if (!menuActive || Gamepad.current == null)
            return;

        Gamepad gamepad = Gamepad.current;

        // Navigation
        bool leftPressed =
            gamepad.dpad.left.wasPressedThisFrame ||
            gamepad.leftStick.left.wasPressedThisFrame;

        bool rightPressed =
            gamepad.dpad.right.wasPressedThisFrame ||
            gamepad.leftStick.right.wasPressedThisFrame;

        if (Time.unscaledTime >= nextNavigationTime)
        {
            if (leftPressed)
            {
                ChangeSelection(-1);
                nextNavigationTime = Time.unscaledTime + navigationDelay;
            }
            else if (rightPressed)
            {
                ChangeSelection(1);
                nextNavigationTime = Time.unscaledTime + navigationDelay;
            }
        }

        // Validate with A / Cross
        if (gamepad.buttonSouth.wasPressedThisFrame)
        {
            SelectItem(selectedIndex);
        }
    }

    // Call this function from another script to display the 3 items
    public void ShowItems()
    {
        float timer = _mealGestion._timer;
        float maxTimer = _mealGestion._maxTimer;

        List<SO_BikeItem> chosenPool = null;

        if (timer <= 0)
        {
            HideItems();
            return;
        }
        else if (timer >= maxTimer / 2f)
        {
            chosenPool = firstPool;
        }
        else if (timer > 0 && timer < maxTimer / 2f)
        {
            chosenPool = secondPool;
        }

        if (chosenPool == null || chosenPool.Count == 0)
        {
            Debug.LogWarning("The selected item pool is empty!");
            return;
        }

        CreateEventSystem();
        CreateCanvas();

        currentItems.Clear();

        // Select 3 random items
        List<SO_BikeItem> availableItems = new List<SO_BikeItem>();

        foreach (SO_BikeItem item in chosenPool)
        {
            if (item != null)
                availableItems.Add(item);
        }

        if (availableItems.Count == 0)
        {
            Debug.LogWarning("No valid items in the selected pool!");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            if (availableItems.Count == 0)
            {
                // Allow duplicates if the pool contains fewer than 3 items
                foreach (SO_BikeItem item in chosenPool)
                {
                    if (item != null)
                        availableItems.Add(item);
                }
            }

            int randomIndex = Random.Range(0, availableItems.Count);

            currentItems.Add(availableItems[randomIndex]);
            availableItems.RemoveAt(randomIndex);
        }

        menuActive = true;
        selectedIndex = 0;

        // Unlock cursor
        previousCursorVisible = Cursor.visible;
        previousCursorLock = Cursor.lockState;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        RefreshDisplay();

        canvas.gameObject.SetActive(true);
    }

    private void CreateEventSystem()
    {
        if (EventSystem.current != null)
            return;

        createdEventSystem = new GameObject(
            "Bike Item EventSystem",
            typeof(EventSystem),
            typeof(InputSystemUIInputModule)
        );

        InputSystemUIInputModule module =
            createdEventSystem.GetComponent<InputSystemUIInputModule>();

        module.AssignDefaultActions();
    }

    private void CreateCanvas()
    {
        if (canvas != null)
            return;

        GameObject canvasObject = new GameObject(
            "Bike Item Selection",
            typeof(RectTransform),
            typeof(Canvas),
            typeof(CanvasScaler),
            typeof(GraphicRaycaster)
        );

        canvas = canvasObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        for (int i = 0; i < 3; i++)
        {
            GameObject slot = new GameObject(
                "Bike Item Slot " + i,
                typeof(RectTransform)
            );

            slot.transform.SetParent(canvas.transform, false);

            RectTransform slotRect = slot.GetComponent<RectTransform>();
            slotRect.sizeDelta = new Vector2(spriteSize.x, spriteSize.y + 100);

            slotRect.anchorMin = new Vector2(0.5f, 0.5f);
            slotRect.anchorMax = new Vector2(0.5f, 0.5f);

            slotRect.anchoredPosition = new Vector2(
                (i - 1) * horizontalSpacing,
                displayHeight
            );

            // Name above sprite
            GameObject textObject = new GameObject(
                "Item Name",
                typeof(RectTransform),
                typeof(TextMeshProUGUI)
            );

            textObject.transform.SetParent(slot.transform, false);

            RectTransform textRect = textObject.GetComponent<RectTransform>();
            textRect.sizeDelta = new Vector2(spriteSize.x + 100, 60);
            textRect.anchoredPosition = new Vector2(
                0,
                spriteSize.y / 2 + 35
            );

            TMP_Text tmp = textObject.GetComponent<TMP_Text>();
            tmp.font = itemFont;
            tmp.fontSize = nameFontSize;
            tmp.color = textColor;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.textWrappingMode = TextWrappingModes.Normal;

            nameTexts[i] = tmp;

            // Sprite button
            GameObject buttonObject = new GameObject(
                "Item Sprite",
                typeof(RectTransform),
                typeof(Image),
                typeof(Button)
            );

            buttonObject.transform.SetParent(slot.transform, false);

            RectTransform buttonRect = buttonObject.GetComponent<RectTransform>();
            buttonRect.sizeDelta = spriteSize;

            Image image = buttonObject.GetComponent<Image>();
            image.preserveAspect = true;

            Button button = buttonObject.GetComponent<Button>();
            button.targetGraphic = image;

            int index = i;

            button.onClick.AddListener(() => SelectItem(index));

            itemButtons[i] = button;
            itemImages[i] = image;
        }

        canvas.gameObject.SetActive(false);
    }

    private void RefreshDisplay()
    {
        for (int i = 0; i < 3; i++)
        {
            SO_BikeItem item = currentItems[i];

            if (item == null)
                continue;

            itemImages[i].sprite = item._sprite;
            nameTexts[i].text = item._name;

            itemImages[i].color =
                i == selectedIndex ? selectedColor : normalColor;

            itemButtons[i].interactable = item._sprite != null;
        }
    }

    private void ChangeSelection(int direction)
    {
        selectedIndex += direction;

        if (selectedIndex < 0)
            selectedIndex = 2;

        if (selectedIndex > 2)
            selectedIndex = 0;

        RefreshDisplay();
    }

    // Select an item by clicking or using the controller
    public void SelectItem(int index)
    {
        if (!menuActive || index < 0 || index >= currentItems.Count)
            return;

        SO_BikeItem selectedItem = currentItems[index];

        if (selectedItem == null || selectedItem._object == null)
        {
            Debug.LogWarning("Selected item or its object is missing!");
            return;
        }

        Transform target = spawnPoint != null ? spawnPoint : transform;

        Instantiate(
            selectedItem._object,
            target.position,
            target.rotation
        );

        HideItems();
    }

    public void HideItems()
    {
        menuActive = false;

        if (canvas != null)
            canvas.gameObject.SetActive(false);

        // Restore cursor state
        Cursor.visible = previousCursorVisible;
        Cursor.lockState = previousCursorLock;
    }

    private void OnDisable()
    {
        menuActive = false;

        if (canvas != null)
            canvas.gameObject.SetActive(false);

        if (createdEventSystem != null)
            Destroy(createdEventSystem);
    }
}
