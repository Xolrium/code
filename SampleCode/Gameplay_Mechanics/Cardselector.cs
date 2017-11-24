using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectPanel : MonoBehaviour {

    /* Stappen:
     * 1. Kaarten inladen
     * 2. Kaart klikken
     * 3. Type en Converted Mana cost in DataPanel
     * 4. Kaarten vervangen
     * 5. Max. 45 kaarten
     */

    public Sprite[] kaarten;
    public string naam;
    public Image card1;
    public Image card2;
    public Image card3;
    public int randomA;
    public int randomB;
    public int randomC;
    DataPanel dPanel;


    void Start()
    {
        dPanel = GetComponent<DataPanel>();
        kaarten = Resources.LoadAll<Sprite>("Kaarten");
        randomA = Random.Range(0, kaarten.Length);
        randomB = Random.Range(0, kaarten.Length);
        randomC = Random.Range(0, kaarten.Length);

        card1.sprite = kaarten[randomA];
        card2.sprite = kaarten[randomB];
        card3.sprite = kaarten[randomC];
    }

    public void SelectedCard()
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "Card1":
                naam = kaarten[randomA].name;
                break;
            case "Card2":
                naam = kaarten[randomB].name;
                break;
            case "Card3":
                naam = kaarten[randomC].name;
                break;
        }
        switch (naam)
        {
            case "negate":
                dPanel.Mana2();   
                break;
            case "collected_company":
                dPanel.Mana4();
                break;
            case "knight_of_the_white_orchid":
                dPanel.Mana2();
                break;
            case "reflector_mage":
                dPanel.Mana3();
                break;
            case "ancient_stirrings":
                dPanel.Mana1();
                break;
            case "days_undoing":
                dPanel.Mana3();
                break;
            case "declaration_in_stone":
                dPanel.Mana2();
                break;
            case "dismember":
                dPanel.Mana3();
                break;
            case "dromokas_command":
                dPanel.Mana2();
                break;
            case "drowner_of_hope":
                dPanel.Mana5();
                break;
            case "eldrazi_displacer":
                dPanel.Mana3();
                break;
            case "eldrazi_skyspawner":
                dPanel.Mana3();
                break;
            case "grafdiggers_cage":
                dPanel.Mana1();
                break;
            case "matter_reshaper":
                dPanel.Mana3();
                break;
            case "nissa_vastwood_seer":
                dPanel.Mana3();
                break;
            case "noble_hierarch":
                dPanel.Mana1();
                break;
            case "path_to_exile":
                dPanel.Mana1();
                break;
            case "reality_smasher":
                dPanel.Mana5();
                break;
            case "rest_in_peace":
                dPanel.Mana2();
                break;
            case "spellskite":
                dPanel.Mana2();
                break;
            case "stony_silence":
                dPanel.Mana2();
                break;
            case "thalia_heretic_cathar":
                dPanel.Mana3();
                break;
            case "thalias_lieutenant":
                dPanel.Mana2();
                break;
            case "thought_knot_seer":
                dPanel.Mana4();
                break;
            case "thraben_inspector":
                dPanel.Mana1();
                break;
            case "tireless_tracker":
                dPanel.Mana3();
                break;
            case "tragic_arrogance":
                dPanel.Mana5();
                break;
            case "worship":
                dPanel.Mana4();
                break;
        }
        randomA = Random.Range(0, kaarten.Length);
        randomB = Random.Range(0, kaarten.Length);
        randomC = Random.Range(0, kaarten.Length);

        card1.sprite = kaarten[randomA];
        card2.sprite = kaarten[randomB];
        card3.sprite = kaarten[randomC];
    }
}
