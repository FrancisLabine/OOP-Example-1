namespace SimulationApp.Core.Models.Domain.Buildings {
    public record BuildingMetadata {
        public int? Interval { get; init; }
        public string Input1 { get; init; }
        public string Input2 { get; init; }
        public string Output { get; init; }
        public int? InputQuantity1 { get; init; }
        public int? InputQuantity2 { get; init; }
        public string Type { get; init; }
        public string IconEmpty { get; init; }
        public string IconLow { get; init; }
        public string IconMedium { get; init; }
        public string IconFull { get; init; }
    }
}
