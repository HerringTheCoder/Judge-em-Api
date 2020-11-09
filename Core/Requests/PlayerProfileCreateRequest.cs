namespace Core.Requests
{
    public class PlayerProfileCreateRequest
    {
        public string Nickname { get; set; }
        public int GameId { get; set; }
        public int? UserId { get; set; }
    }
}
