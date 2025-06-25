using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.ParticleSystemJobs;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    [Header("Windmills")]
    public GameObject[] objectsWithScripts;

    [Header("Ui")]
    public Selectable[] uiElementsToDisable;

    [Header("Colour")]
    public GameObject colourCanvas;
    public Color _goalColour;
    public Color[] _colorsArray;

    [Header("RandomName")]
    public GameObject randomNameCanvas;
    public string randomName;
    public TMP_Text randomNameText;

    // Liste der männlichen Adjektive
    private string[] adjektive = new string[]
    {
        "verrückter", "lustiger", "komischer", "schräger", "alberner", "zappeliger", "flippiger", "witziger", "seltsamer", "spaßiger",
        "plumper", "lauter", "überdrehter", "durchgeknallter", "schriller", "cooler", "lässiger", "stylischer", "entspannter", "smarter",
        "trendiger", "moderner", "souveräner", "chilliger", "lockerer", "selbstsicherer", "eleganter", "flinker", "glänzender", "wilder",
        "starker", "mächtiger", "brutaler", "rasender", "donnernder", "explosiver", "brennender", "unaufhaltbarer", "tapferer", "mutiger",
        "furchtloser", "heldenhafter", "krasser", "zäher", "unbesiegbarer", "robuster", "harter", "frostiger", "sonniger", "windiger",
        "stürmischer", "erdiger", "felsiger", "nebliger", "feuriger", "wasserreicher", "dunkler", "gruseliger", "finsterer", "geisterhafter",
        "spukhafter", "unheimlicher", "totenstiller", "schattenhafter", "kluger", "schlauer", "neugieriger", "cleverer", "tüftelnder",
        "logischer", "grübelnder", "analytischer", "gelehrter", "nerdiger", "flauschiger", "zuckersüßer", "niedlicher", "funkelnder",
        "kuscheliger", "fröhlicher", "glitzernder", "zarter", "hopsiger", "kulleriger", "bunter", "schimmernder", "magischer", "verträumter",
        "freundlicher", "witzelnder", "geheimer", "mysteriöser", "unsichtbarer", "silberner", "goldener", "stahlharter", "verräterischer",
        "leuchtender", "elektrischer", "mechanischer", "biestiger", "schlammiger", "kantiger", "schneller", "leiser", "aggressiver",
        "geduldiger", "listiger", "gefährlicher", "selbstloser", "frecher", "verschrobener", "verwegener", "legendärer", "epischer",
        "chaotischer", "genialer", "verpeilter", "nasser", "trockener", "blinder", "tauber", "wandelbarer", "fliegender", "tanzender",
        "singender", "brüllender", "jagender", "zitternder", "schnarchender", "gähnender", "lachender", "weinender", "träumender"
    };

    // Liste der männlichen Tiere
    private string[] tiere = new string[]
    {
        "Löwe", "Tiger", "Bär", "Wolf", "Fuchs", "Hirsch", "Eber", "Rabe", "Panther", "Adler",
        "Falke", "Geier", "Stier", "Hund", "Kater", "Hahn", "Pfau", "Widder", "Ziegenbock", "Dachs",
        "Marder", "Schakal", "Igel", "Hase", "Maulwurf", "Biber", "Otter", "Affe", "Gorilla", "Orang-Utan",
        "Schimpanse", "Elefant", "Wal", "Delphin", "Hai", "Krake", "Fisch", "Pavian", "Yak", "Kojote",
        "Büffel", "Zebra", "Nashorn", "Elch", "Mammut", "Drache", "Greif", "Minotaurus", "Zentaur", "Werwolf",
        "Vogel", "Pinguin", "Strauß", "Kranich", "Schwan", "Spatz", "Specht", "Uhu", "Kauz", "Kondor",
        "Luchs", "Wiesel", "Frettchen", "Kaninchen", "Kamel", "Esel", "Pony", "Ochse", "Rind", "Maultier",
        "Frosch", "Kröterich", "Molch", "Leguan", "Iltis", "Käfer", "Skorpion", "Marienkäfer", "Schmetterling",
        "Käuzchen", "Rüsselkäfer"
    };


    void Start()
    {
        foreach (GameObject obj in objectsWithScripts)
        {
            MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (script == this) continue; //failsafe wenn Script is Self
                script.enabled = false;
            }
        }

        foreach (Selectable ui in uiElementsToDisable)
        {
            ui.interactable = false;
        }
    }

    public void ActivateRandomName()
    {
        randomNameCanvas.SetActive(true);
        GameObject callerx = EventSystem.current.currentSelectedGameObject;
        if (callerx != null)
        {
            callerx.SetActive(false);
        }

    }

    public void ChooseRandomName()
    {
        randomName = adjektive[Random.Range(0, adjektive.Length)] + " " + tiere[Random.Range(0, tiere.Length)];
        randomNameText.text = randomName;
        StartCoroutine(WaitTime());
    }
    public void ActivateCoulorCanvas()
    {
        colourCanvas.SetActive(true);
        
    }

    public void SelectColorGoal(int a)
    {
        foreach (GameObject obj in objectsWithScripts)
        {
            MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (script == this) continue; //failsafe wenn Script is Self
                script.enabled = true;
            }
        }

        foreach (Selectable ui in uiElementsToDisable)
        {
            ui.interactable = true;
        }

        colourCanvas.SetActive(false);

        _goalColour = _colorsArray[a];

    }
    void Update()
    {

    }

    public IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2);
        randomNameCanvas.SetActive(false);
        ActivateCoulorCanvas();
    }
}
