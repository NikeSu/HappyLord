using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatQuad : MonoBehaviour {

    private int unitID;
    private int posx;
    private int posy;
    private int energyLevel;
    private int bulletCount;
    private byte v;

    public CombatQuad(int unitID)
    {
        this.unitID = unitID;
    }

    public static CombatQuad newFromSFSObject(ISFSObject sfso)
    {
        CombatQuad combatQuad = new CombatQuad(sfso.GetByte("id"));

        combatQuad.posx = sfso.GetShort("px");
        combatQuad.posy = sfso.GetShort("py");
        combatQuad.energyLevel = sfso.GetByte("el");
        combatQuad.bulletCount = sfso.GetByte("bc");

        return combatQuad;
    }
}
