using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public PlayerController Player;
    public GameObject CineCam;
    void Awake(){
        GameManager.Instance.Player = Player;
        GameManager.Instance.CineCam = CineCam;
    }

}
