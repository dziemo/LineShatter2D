using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteSetter : MonoBehaviour
{
    public Material playerMat, backgroundMat, badSegmentsMat;

    public List<Palette> palettes = new List<Palette>();

    private void Start()
    {
        SetPalette();
    }

    void SetPalette ()
    {
        var palette = palettes[Random.Range(0, palettes.Count)];

        playerMat.color = palette.playerColor;
        backgroundMat.color = palette.backgroundColor;
        badSegmentsMat.color = palette.badSegmentsColor;
    }

}
