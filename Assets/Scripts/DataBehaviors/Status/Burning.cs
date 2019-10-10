public class Burning : Status 
{
    private readonly float duration;
    private readonly float interval;
    private float timer;
    public Burning(int sourceID, float duration, float interval, bool stacks) : base(sourceID, stacks)
    {
        this.duration = duration;
        this.interval = interval;
        timer = duration;
    }

    public override bool Stacks => false;

    public override bool Equals(object obj)
    {
        if (!(obj is Burning))
            return false;
        var other = obj as Burning;
        if (this.duration == other.duration && this.interval == other.interval && this.stacks == other.stacks && this.sourceID == other.sourceID)
            return true;
        return false;
    }

    public override void Extend()
    {
        timer += duration;
    }

    protected override void Apply(IUnit owner)
    {
        
    }

    protected override bool Expired()
    {
        return timer <= 0;
    }

    protected override void Process(IUnit owner)
    {
        timer -= GameTime.deltaTime;
        if (timer % interval == 0)
        {
            owner.TakeDamage(SourceID, new Damage(2,0,0,0));
        }
    }

    protected override void Remove(IUnit unit)
    {
        
    }
}
