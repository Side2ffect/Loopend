namespace Target
{
    interface ITarget
    {
        public void Hit();
    }

    interface ITargetSpawnPoint
    {
        public int Score { get; set; }
        public TargetSize Size { get; set; }
        public float RespawnTime { get; set; }
        public void SpawnTarget();
    }

    public enum TargetSize
    {
        TargetSmall,
        TargetMedium,
        TargetLarge,
        air_TargetSmall,
        air_TargetMedium,
        air_TargetLarge
    }
}