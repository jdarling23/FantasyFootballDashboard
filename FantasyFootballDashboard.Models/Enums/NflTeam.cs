namespace FantasyFootballDashboard.Models.Enums
{
    public enum NflTeam
    {
        FreeAgent,
        ArizonaCardinals,
        AtlantaFalcons,
        BalitmoreRavens,
        BuffaloBills,
        CarolinaPanthers,
        ChicagoBears,
        CincinnatiBengals,
        ClevelandBrowns,
        DallasCowboys,
        DenverBroncos,
        DetroitLions,
        GreenBayPackers,
        HoustonTexans,
        IndianapolisColts,
        JacksonvilleJaguars,
        KansasCityCheifs,
        LasVegasRaiders,
        LosAngelesChargers,
        LosAngelesRams,
        MiamiDolphins,
        MinnesotaVikings,
        NewEnglandPatriots,
        NewOrleansSaints,
        NewYorkGiants,
        NewYorkJets,
        PhilidelphiaEagles,
        PittsburghSteelers,
        SanFrancisco49ers,
        SeattleSeahawks,
        TampaBayBuccaneers,
        TenneseeTitans,
        WashingtonFootballTeam,
    }

    public static class TeamMapper
    {
        public static NflTeam MapTeam(string team)
        {
            switch (team)
            {
                case ("ARI"):
                    return NflTeam.ArizonaCardinals;
                case ("ATL"):
                    return NflTeam.AtlantaFalcons;
                case ("BAL"):
                    return NflTeam.BalitmoreRavens;
                case ("BUF"):
                    return NflTeam.BuffaloBills;
                case ("CAR"):
                    return NflTeam.CarolinaPanthers;
                case ("CHI"):
                    return NflTeam.ChicagoBears;
                case ("CIN"):
                    return NflTeam.CincinnatiBengals;
                case ("CLE"):
                    return NflTeam.ClevelandBrowns;
                case ("DAL"):
                    return NflTeam.DallasCowboys;
                case ("DEN"):
                    return NflTeam.DenverBroncos;
                case ("DET"):
                    return NflTeam.DetroitLions;
                case ("GBP"):
                    return NflTeam.GreenBayPackers;
                case ("HOU"):
                    return NflTeam.HoustonTexans;
                case ("IND"):
                    return NflTeam.IndianapolisColts;
                case ("JAX"):
                    return NflTeam.JacksonvilleJaguars;
                case ("KCC"):
                    return NflTeam.KansasCityCheifs;
                case ("LVR"):
                    return NflTeam.LasVegasRaiders;
                case ("LAC"):
                    return NflTeam.LosAngelesChargers;
                case ("LAR"):
                    return NflTeam.LosAngelesRams;
                case ("MIA"):
                    return NflTeam.MiamiDolphins;
                case ("MIN"):
                    return NflTeam.MinnesotaVikings;
                case ("NEP"):
                    return NflTeam.NewEnglandPatriots;
                case ("NOS"):
                    return NflTeam.NewOrleansSaints;
                case ("NYG"):
                    return NflTeam.NewYorkGiants;
                case ("NYJ"):
                    return NflTeam.NewYorkJets;
                case ("PHI"):
                    return NflTeam.PhilidelphiaEagles;
                case ("PIT"):
                    return NflTeam.PittsburghSteelers;
                case ("SFF"):
                    return NflTeam.SanFrancisco49ers;
                case ("SEA"):
                    return NflTeam.SeattleSeahawks;
                case ("TBB"):
                    return NflTeam.TampaBayBuccaneers;
                case ("TEN"):
                    return NflTeam.TenneseeTitans;
                case ("WAS"):
                    return NflTeam.WashingtonFootballTeam;
                default:
                    return NflTeam.FreeAgent;
            }
        }
    }
}
