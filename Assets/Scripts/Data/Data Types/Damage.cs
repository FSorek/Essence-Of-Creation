
[System.Serializable]
public struct Damage
{
    public int Fire;
    public int Air;
    public int Water;
    public int Earth;

    public Damage(int fire, int air, int water, int earth)
    {
        Fire = fire;
        Air = air;
        Water = water;
        Earth = earth;
    }
}
