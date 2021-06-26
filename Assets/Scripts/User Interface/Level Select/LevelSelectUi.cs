using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using User_Interface;

public partial class LevelSelectUi : BaseUi
{
    [Header("Level Select UI")]
    [SerializeField] TextMeshProUGUI title;

    [SerializeField] Transform worldButton, levelButton;
    [SerializeField] Transform worldContainer, levelContainer;
    [SerializeField] GameObject worldExitButton, levelExitButton;

    [SerializeField] GameObject backButton, forwardButton;
    private int numOfActiveButtons = 0, numOfWorldButtons = 0, currentSelectedButton = 0;

    private void Start()
    {
        MusicManager.i.ChangeMusicTrack(0);

        RenderWorldSelect();
    }

    /// <summary>
    /// Move entire World or Level Select container to a chosen side
    /// </summary>
    /// <param name="direction">-1 = Left, 1 = Right</param>
    public void MoveSelection(int direction)
    {
        int moveAmount = direction * -200;
        currentSelectedButton += direction;

        if (worldContainer.gameObject.activeInHierarchy)
        {
            Vector2 newPos = new Vector2(worldContainer.localPosition.x + moveAmount, worldContainer.localPosition.y);
            worldContainer.localPosition = newPos;
        }
        else if (levelContainer.gameObject.activeInHierarchy)
        {
            Vector2 newPos = new Vector2(levelContainer.localPosition.x + moveAmount, levelContainer.localPosition.y);
            levelContainer.localPosition = newPos;
        }
        RefreshBackForwardButtons();
    }

    /// <summary>
    /// Show the Back or Forward buttons in world/level select only if the selector can be moved
    /// </summary>
    private void RefreshBackForwardButtons()
    {
        backButton.SetActive(true);
        forwardButton.SetActive(true);

        if (currentSelectedButton == 0)
            backButton.SetActive(false);
        if (currentSelectedButton == numOfActiveButtons - 3)
            forwardButton.SetActive(false);

        print(currentSelectedButton + " " + numOfActiveButtons);
    }

    public void RenderWorldSelect()
    {
        numOfActiveButtons = 0; currentSelectedButton = 0;
        float baseEntryX = 0f, offsetEntryX = 200f;
        foreach (World world in LevelHandling.worlds)
        {
            Transform worldEntry = Instantiate(worldButton, worldContainer);
            RectTransform worldEntryRect = worldEntry.GetComponent<RectTransform>();
            worldEntryRect.anchoredPosition = new Vector2(baseEntryX + offsetEntryX * world.WorldNum, 0);

            worldEntry.Find("WorldText").GetComponent<TextMeshProUGUI>().text = world.WorldName;
            worldEntry.Find("HumansText").GetComponent<TextMeshProUGUI>().text = "0/" + world.TotalHumansInWorld;
            worldEntry.GetComponent<Button>().onClick.AddListener(() => RenderLevelSelect(world));

            numOfActiveButtons += 1;
        }
        numOfWorldButtons = numOfActiveButtons;
        RefreshBackForwardButtons();
    }

    public void RenderLevelSelect(World worldChosen)
    {
        title.text = worldChosen.WorldName;
        worldContainer.gameObject.SetActive(false);
        levelContainer.gameObject.SetActive(true);
        worldExitButton.SetActive(false);
        levelExitButton.SetActive(true);

        numOfActiveButtons = 0; currentSelectedButton = 0;
        float baseEntryX = 0f, offsetEntryX = 200f;
        foreach (Level level in worldChosen.LevelsInWorld)
        {
            Transform levelEntry = Instantiate(levelButton, levelContainer);
            RectTransform worldEntryRect = levelEntry.GetComponent<RectTransform>();
            worldEntryRect.anchoredPosition = new Vector2(baseEntryX + offsetEntryX * level.LevelNum, 0);

            levelEntry.Find("WorldText").GetComponent<TextMeshProUGUI>().text = level.LevelName;
            levelEntry.Find("HumansText").GetComponent<TextMeshProUGUI>().text = "0/" + level.HumansPresent;
            levelEntry.GetComponent<LevelSelect_LevelButton>().attachedScene = level.LevelString;

            numOfActiveButtons += 1;
        }
        RefreshBackForwardButtons();
    }

    public void FromLevelToWorldSelect()
    {
        title.text = "Select a World";
        levelContainer.gameObject.SetActive(false);
        worldContainer.gameObject.SetActive(true);

        worldExitButton.SetActive(true);
        levelExitButton.SetActive(false);

        numOfActiveButtons = numOfWorldButtons; currentSelectedButton = 0;
        RefreshBackForwardButtons();

        // Destroy level buttons that were rendered in levelContainer
        foreach (Transform existingLevelBtn in levelContainer.transform)
        {
            if (existingLevelBtn.name != "ButtonBackToWorldSelect")
                Destroy(existingLevelBtn.gameObject);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu CrossEdit");
    }

    public void PlayTransition()
    {
        levelTransitionOverlay.SetBool("levelEndedOrDead", true);
    }
}
