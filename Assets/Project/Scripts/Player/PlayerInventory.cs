using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int currentRoots = 30;

    [SerializeField]
    GameObject[] extraHearts;

    [SerializeField]
    GameObject[] voidHearts;

    private int extraheartsCounter = 0;

    public TextMeshProUGUI[] currentRootsText;

    private PlayerHealthSystem phs;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < currentRootsText.Length; i++)
        {
            currentRootsText[i].text = currentRoots.ToString();
        }

        phs = this.GetComponent<PlayerHealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < currentRootsText.Length; i++)
        {
            currentRootsText[i].text = currentRoots.ToString();
        }
    }

    public void IncreaseMaxHearts()
    {
        if (currentRoots >= 5)
        {
            currentRoots -= 5;

            for (int i = 0; i < currentRootsText.Length; i++)
            {
                currentRootsText[i].text = currentRoots.ToString();
            }
            phs.MaxHealth++;
            phs.heartList.Add(extraHearts[extraheartsCounter]);
            extraHearts[extraheartsCounter].SetActive(true);
            voidHearts[extraheartsCounter].SetActive(true);
            extraheartsCounter++;
            phs.health = phs.MaxHealth;
            phs.RespawnHeal();
        }
    }
}
