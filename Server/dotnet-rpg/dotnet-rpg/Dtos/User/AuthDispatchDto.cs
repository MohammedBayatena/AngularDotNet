namespace dotnet_rpg.Dtos.User
{
    public class AuthDispatchDto
    {
        public string Username { get; set; }
        public int Id { get; set; }
        public string Token { get; set; }

        public AuthDispatchDto(int Id, string Username, string token)
        {
            this.Id = Id;
            this.Username = Username;
            this.Token = token;
        }
    }
}