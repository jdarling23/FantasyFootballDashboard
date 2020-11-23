namespace FantasyFootballDashboard.Models.Enums
{
    public enum Position
    {
        None,
        QuarterBack,
        RunningBack,
        WideReceiver,
        TightEnd,
        Kicker,
        DefenseSpecialTeams
    }

    public static class PositionMapper
    {
        public static Position MapPosition(string position)
        {
            switch (position)
            {
                case ("QB"):
                    return Position.QuarterBack;
                case ("RB"):
                    return Position.RunningBack;
                case ("WR"):
                    return Position.WideReceiver;
                case ("TE"):
                    return Position.TightEnd;
                case ("Def"):
                    return Position.DefenseSpecialTeams;
                case ("PK"):
                    return Position.Kicker;
                default:
                    return Position.None;
            }
        }
    }
}
