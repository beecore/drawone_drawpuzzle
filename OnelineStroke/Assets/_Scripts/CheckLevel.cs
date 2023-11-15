using UnityEngine;
using UnityEngine.UI;

public class CheckLevel : MonoBehaviour
{
    public GameObject prefab;
    public Transform parentPosition;
    private RectTransform scrollTransform;
    private int level;
    private int levelCurrent = 0;
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    private RectTransform target;

    private void Start()
    {
        levelCurrent = PlayerData.instance.CurrentLevel;
        level = LevelData.totalLevelsPerWorld;
        CreateList();
        SnapTo(scrollRect, target);
        scrollRect.vertical = false;
    }

    private void CreateList()
    {
        int MAXLENGTH = 6;
        int min = 0;
        int max = 0;

        if (levelCurrent >= 3)
        {
            min = levelCurrent - 2;
            max = min + MAXLENGTH;
        }
        else
        {
            min = 0;
            max = min + MAXLENGTH;
        }
        if (max > level)
        {
            max = level;
            min = max - MAXLENGTH;
        }

        for (int i = max - 1; i >= min; i--)
        {
            GameObject obj = Instantiate(prefab, parentPosition);
            ItemLevel itemLevel = obj.GetComponent<ItemLevel>();
            itemLevel.textMeshProUGUI.SetText((i + 1).ToString());
            if (i == 0)
            {
                itemLevel.Path_Bottom.SetActive(false);
            }

            if ((i + 1) == levelCurrent)
            {
                itemLevel.Stage_Lock.SetActive(false);
                itemLevel.Stage_Active.SetActive(true);
                itemLevel.Stage_Actived.SetActive(false);
                target = obj.GetComponent<RectTransform>();
            }
            else if ((i + 1) > levelCurrent)
            {
                itemLevel.Stage_Lock.SetActive(true);
                itemLevel.Stage_Active.SetActive(false);
                itemLevel.Stage_Actived.SetActive(false);
            }
            else
            {
                itemLevel.Stage_Lock.SetActive(false);
                itemLevel.Stage_Active.SetActive(false);
                itemLevel.Stage_Actived.SetActive(true);
            }
        }
    }

    public void SnapTo(ScrollRect scrollRect, RectTransform target, float offsetX = 0f, float offsetY = 0f)
    {
        Canvas.ForceUpdateCanvases();

        Vector2 contentPosition = scrollRect.transform.InverseTransformPoint(scrollRect.content.position);
        Vector2 newPosition = scrollRect.transform.InverseTransformPoint(target.position);
        newPosition = new Vector2(newPosition.x + offsetX, newPosition.y + offsetY);
        if (!scrollRect.horizontal)
            newPosition.x = contentPosition.x;

        if (!scrollRect.vertical)
            newPosition.y = contentPosition.y;

        Vector2 localOff = (contentPosition - newPosition);
        scrollRect.content.anchoredPosition = new Vector2(newPosition.x, 0.5f * localOff.y);
    }
}