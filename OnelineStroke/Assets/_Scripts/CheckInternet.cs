using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckInternet : MonoBehaviour
{
    public GameObject dialogNetWork;

    private void Start()
    {
        IsAvailableInternet();
    }

    public void IsAvailableInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            dialogNetWork.SetActive(true);
        }
        else
        {
            dialogNetWork.SetActive(false);
            StartCoroutine(LoadLevelAfterDelay(1.5f));
        }
    }

    private IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1);
    }
}