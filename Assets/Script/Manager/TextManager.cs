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

        LvlEntryDial.Add("Welcome to your first day at Star Delivery we hope you follow the trainning. The only things left for you to learn are the button to stop" +
                         " the belt it will temporary stop the belt but hinder your productivity as no package will be brought to the belt for 10 seconds, that bring us to the second point be productive engough and you will be employee of the month");
        LvlEntryDial.Add("You survive your first day and didn't quit maybe you've got some potential after all. Let's see how you'll do today because now some package will have bombs attached to them good luck.");
        LvlEntryDial.Add("Let's begin this third day of work, third time the charm as we say. This one as a nice slope on the belt isn't it exiting sadly no new challenge for you just a bit more bomb.");
        LvlEntryDial.Add("Hello and welcome to your fourth day in hell, sorry in our company. Today a little challenge there's two entry point for the parcel to come on the belt and some parcel will be fragile so be carefull.");
        LvlEntryDial.Add("I have to admit you're performance those last four days was slightly impressive so today your assign on a hard workplace, because the shuttle are a little far from the belt but I believe in you. At least I'm contractly oblige to say so.");
        LvlEntryDial.Add("You're still showing up after five day working under my care, I hope you don't use stimulant to keep up the pace. Anyway your perfomance speak for you so now you will have to handle suspicious package too.");
        LvlEntryDial.Add("I quite can't believe it the stat say if you fulfill your abjective today you will become the employee of the month in one week. Well you're assign to my workplace so I'm confident you will fail.");

        //LvlExitDial.Add(""); //non utilisé

        barkDial.Add("");
    }
}
