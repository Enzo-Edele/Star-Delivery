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
        tutoDial.Add("The belt bring parcel grab the one coming and wait for instructions.");
        tutoDial.Add("There's a stickers at the top of each parcel it represent the destination of said packages.");
        tutoDial.Add("Up there there is a shuttle, deliver the parcel there, on the screen beside there's an image it shows the destination of the shuttle" +
                     " if the image matches the sticker put the parcel in the shuttle.");
        tutoDial.Add("To put the parcel in the shuttle drop it on the plateform near it, and remember if the package is not supposed to go in you will get" +
                     " a warning three warnings and you're fire. Your chances can be seen in the upper-left corner of the screen");
        tutoDial.Add("Now the package is in the shuttle, note that each shuttle can store 4 parcel before starting to overload, once overload it can take" +
                     " 2 more but it will take more time for the shuttle to come back, now push the button to send the shuttle. While it's gone you can't drop packages in");
        tutoDial.Add("Some parcel are special, fragile parcel need to be handle carefully, to do so when grabing one press shift to move, you will be slower" +
                     " but if you don't the package will break and will need to be destroy and not send. For the pupose of learning if break it will be destroy instantly.");
        tutoDial.Add("Now that you grab it store the parcel in the shelf on the green area, storing parcel can be usefull if the shuttle is gone or if the parcel has a bomb but more on that latter");
        tutoDial.Add("The next parcel will have a wrong destination meaning none of the shuttle will be able to deliver the parcel so destroy it by droping it on" +
                     " crusher on the yellow area.");
        tutoDial.Add("Now bombs, sometime a bomb will be attach you will need to disarm it before destroying the parcel regardless of it's destination, to do so" +
                     " go to the diffuse table, red area, and prees the button of the bomb matching the order shown on the table.");
        tutoDial.Add("Finnaly suspicious parcel, those are parcel containing illegal good, check the machine on the belt if it shows a red 'X' then the parcel must be destroyed regardless of it's destination.");
        tutoDial.Add("Congrats you pass all the test now proceed to the next room and press the button to begin the trainning in real condition it will last 2 minutes if you remember the instructions you should do just fine.");

        LvlEntryDial.Add("");

        LvlExitDial.Add("");

        barkDial.Add("");
    }
}
