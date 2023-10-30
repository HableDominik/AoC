namespace AoC.src.Puzzles._2015.Day14
{
    internal class Reindeer
    {
        internal string Name { get; init; }
        internal int Speed { get; init; }
        internal int Fly { get; init; }
        internal int Pause { get; init; }
        internal bool Flies { get; set; }
        internal int CurrentRest { get; set; }
        internal int Distance { get; set; }
        internal int Points { get; set; }

        public Reindeer(string name, int speed, int fly, int pause)
        {
            Name = name;
            Speed = speed;
            Fly = fly;
            Pause = pause;
            Flies = false;
        }

        public void Tick()
        {
            if (CurrentRest is 0)
            {
                Flies = !Flies;
                if (Flies) CurrentRest = Fly;
                else CurrentRest = Pause;
            }
            if (Flies) Distance += Speed;
            CurrentRest--;
        }
    }
}
