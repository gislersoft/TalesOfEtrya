using System.Collections;
using UnityEngine;

public class SpawnLetterBehavior : MonoBehaviour {

    Transform letterHolderTransform;
    GameObject letter;

    readonly string[] abc = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    private void Start()
    {
        letterHolderTransform = transform;
    }

    public IEnumerator SpanwTrailOfLetters()
    {

        //int randomLetter = Random.Range(0, abc.Length);
        //var selectedLetterRequest = Resources.LoadAsync("LetterHolder" + abc[randomLetter]) as ResourceRequest;
        var selectedLetterRequest = Resources.LoadAsync("Coin") as ResourceRequest;
        yield return selectedLetterRequest;

        var selectedLetter = selectedLetterRequest.asset as GameObject;

        letter = Instantiate(selectedLetter, Vector3.zero, Quaternion.identity, transform);
        letterHolderTransform.gameObject.SetActive(false);
        letterHolderTransform.gameObject.SetActive(true);

    }
}
