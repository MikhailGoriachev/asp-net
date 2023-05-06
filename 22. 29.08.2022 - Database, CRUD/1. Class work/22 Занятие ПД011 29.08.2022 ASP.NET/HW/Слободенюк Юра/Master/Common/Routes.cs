namespace Master.Common;


public static class Routes
{
    public static class Home
    {
        public const string Index = "/";
    }


    public static class People
    {
        private const string Base = nameof(People);
        public const string Index = $"{Base}";
        public const string Details = $"{Base}/{{id:int}}";
        public const string ByMass = $"{Base}/ByMass";
        public const string ByHomeworld = $"{Base}/ByHomeworld";
        public const string MassAscending = $"{Base}/MassAscending";
    }


    public static class Planets
    {
        private const string Base = nameof(Planets);
        public const string Index = $"{Base}";
        public const string Details = $"{Base}/{{id:int}}";
        public const string ByDiameter = $"{Base}/ByDiameter";
        public const string ByTerrain = $"{Base}/ByTerrain";
    }
}