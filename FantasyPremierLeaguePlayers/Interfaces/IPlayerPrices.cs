namespace FantasyPremierLeague
{
    public interface IPlayerPrices
    {
        bool InsertPlayerPrices(Player player);
        bool UpdatePlayerPrices(Player player);
        bool DeletePlayerPrices(int playerId);
    }
}
