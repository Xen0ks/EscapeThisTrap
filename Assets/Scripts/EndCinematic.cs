using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndCinematic : MonoBehaviour
{
    private void Update()
    {
        if(GetComponent<PlayableDirector>().time > 8)
        {
            SceneManager.LoadScene(0);
        }
    }
}
