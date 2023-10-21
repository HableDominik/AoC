namespace AoC.src.Puzzles._2015.Day02
{
    internal class Present
    {
        private readonly int l, w, h;
        public Present(string dimensions)
        {
            (l, w, h) = getLWH(dimensions);
        }

        public int CalculateSurface()
        {
            var smallestSide = GetSmallesSide();
            var surface = 2 * l * w + 2 * w * h + 2 * h * l;
            return surface + smallestSide;
        }

        private int GetSmallesSide() => Math.Min(Math.Min(l * w, w * h), h * l);

        private (int l, int w, int h) getLWH(string present)
        {
            var lwh = present.Split('x');
            var l = Int32.Parse(lwh[0]);
            var w = Int32.Parse(lwh[1]);
            var h = Int32.Parse(lwh[2]);

            return (l, w, h);
        }

        public int CalculateRibbon()
        {
            var mask = GetMask();
            var wrap = 2 * l * mask[0] + 2 * w * mask[1] + 2 * h * mask[2];
            var bow = l * w * h;
            return wrap + bow;
        }

        private int[] GetMask()
        {
            int[] mask = { 1, 1, 1 };
            if (l >= w && l >= h) mask[0] = 0;
            else if (w >= l && w >= h) mask[1] = 0;
            else if (h >= l && h >= w) mask[2] = 0;
            else throw new InvalidProgramException();
            return mask;
        }
    }
}
