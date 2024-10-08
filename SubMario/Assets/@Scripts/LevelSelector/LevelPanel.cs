using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    int stageIndex;
    public Image StageThumb;
    public TMP_Text TextTitle;

    public void SetLevelInformation(int stageIndex, Sprite thumbnail, string title)
    {
        TextTitle.text = title;
        this.stageIndex = stageIndex;
        StageThumb.sprite = thumbnail;
    }
    public void StageStart()
    {
        LevelManager.Instance.StartLevel(stageIndex);
    }
}
