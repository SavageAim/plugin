using System;

namespace SavageAim;
public struct InGameCharacterData
{
    String name;
    String currentHead;
    String currentBody;
    String currentHands;
    String currentLegs;
    String currentFeet;
    String currentEarrings;
    String currentNecklace;
    String currentBracelet;
    String currentRightRing;
    String currentLeftRing;

    public static unsafe InGameCharacterData FetchData()
    {
        return new InGameCharacterData(); 
    }
}
