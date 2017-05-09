using UnityEngine;


[System.Serializable]
public struct RationalVector2
{
    public Rational x;
    public Rational y;

    public RationalVector2(Rational x, Rational y)
    {
        this.x = x;
        this.y = y;
    }


    public static implicit operator Vector2(RationalVector2 rv2)
    {
        return new Vector2(rv2.x, rv2.y);
    }
}