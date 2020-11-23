namespace FantasyFootballDashboard.APIConnector.Interfaces
{
    /// <summary>
    /// Abstract class defining wha fields we want from a reference player
    /// </summary>
    public abstract class ReferencePlayerBase
    {
        public abstract int PlayerId { get; set; }

        public abstract string Name { get; set; }

        public abstract string Team { get; set; }

        public abstract string Position { get; set; }

        public abstract string PositionCategory { get; set; }

        public abstract string PhotoUrl { get; set; }

        public abstract string Status { get; set; }

        public abstract int? JerseyNumber { get; set; }

        public abstract int? YearsInLeague { get; set; }

        public abstract string College { get; set; }

        public abstract double? AverageDraftPosition { get; set; }

        public abstract int? ByeWeek { get; set; }
    }
}
