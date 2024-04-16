using UnityEngine;
using UnityEngine.Playables;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Level lvl = transform.parent.GetComponent<Level>();
        lvl.GetComponent<PlayableDirector>().Play();
    }
}
