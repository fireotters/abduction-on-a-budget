using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using User_Interface;

public partial class LevelSelectUi : BaseUi
{
    [SerializeField] Transform worldButton, levelButton;
    [SerializeField] Transform worldContainer, levelContainer;

    [SerializeField] TextMeshProUGUI title;

    private void Start()
    {
        MusicManager.i.ChangeMusicTrack(0);

        RenderWorldSelect();
    }

    public void RenderWorldSelect()
    {
        float baseEntryX = 0f, offsetEntryX = 200f;
        foreach (World world in LevelHandling.worlds)
        {
            Transform worldEntry = Instantiate(worldButton, worldContainer);
            RectTransform worldEntryRect = worldEntry.GetComponent<RectTransform>();
            worldEntryRect.anchoredPosition = new Vector2(baseEntryX + offsetEntryX * world.WorldNum, 0);

            worldEntry.Find("WorldText").GetComponent<TextMeshProUGUI>().text = world.WorldName;
            worldEntry.Find("HumansText").GetComponent<TextMeshProUGUI>().text = "0/" + world.TotalHumansInWorld;
            worldEntry.GetComponent<Button>().onClick.AddListener(() => RenderLevelSelect(world));
        }
    }

    public void RenderLevelSelect(World worldChosen)
    {
        title.text = worldChosen.WorldName;
        worldContainer.gameObject.SetActive(false);
        levelContainer.gameObject.SetActive(true);

        float baseEntryX = 0f, offsetEntryX = 200f;
        foreach (Level level in worldChosen.LevelsInWorld)
        {
            Transform levelEntry = Instantiate(levelButton, levelContainer);
            RectTransform worldEntryRect = levelEntry.GetComponent<RectTransform>();
            worldEntryRect.anchoredPosition = new Vector2(baseEntryX + offsetEntryX * level.LevelNum, 0);

            levelEntry.Find("WorldText").GetComponent<TextMeshProUGUI>().text = level.LevelName;
            levelEntry.Find("HumansText").GetComponent<TextMeshProUGUI>().text = "0/" + level.HumansPresent;
            levelEntry.GetComponent<LevelSelect_LevelButton>().attachedScene = level.LevelString;
        }
    }

    public void FromLevelToWorldSelect()
    {
        title.text = "Select a World";
        levelContainer.gameObject.SetActive(false);
        worldContainer.gameObject.SetActive(true);

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
