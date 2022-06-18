using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public List<string> tutoDial;
    public List<string> LvlEntryDial;
    public List<string> LvlExitDial;
    public List<string> barkDial;

    public static TextManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        tutoDial.Add("Use ZQSD to move and space to Jump.");
        tutoDial.Add("The belt bring packages grab the one coming and wait instructions.");
        tutoDial.Add("There's a stickers it represent the destination of said packages");

        LvlEntryDial.Add("");

        LvlExitDial.Add("");

        barkDial.Add("");
    }
}
