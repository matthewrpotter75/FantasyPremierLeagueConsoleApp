namespace FantasyPremierLeague
{
    public interface IPlayer
    {
        bool InsertPlayer(Player player);
        bool UpdatePlayer(Player player);
        bool DeletePlayer(int playerId);
    }
}
