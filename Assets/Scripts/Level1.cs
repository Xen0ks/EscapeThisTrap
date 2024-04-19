using UnityEngine;

public class Level1 : MonoBehaviour
{
    public GameObject[] objectsToDestroyOnLever;

    Player player;

    public Transform tpPointAfterLever;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    public void Lever()
    {
        foreach(GameObject obj in objectsToDestroyOnLever)
        {
            Destroy(obj);
            StartCoroutine(player.Teleport(tpPointAfterLever.position));
        }
    }
}
