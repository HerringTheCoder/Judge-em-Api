using System;
using System.Collections.Generic;
using System.Text;
using Storage.Tables;

namespace Core.Dto
{
    public class PlayerProfileDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public UserDto User { get; set; }

        public PlayerProfileDto(PlayerProfile playerProfile)
        {
            Id = playerProfile.Id;
            Nickname = playerProfile.Nickname;
            User = new UserDto(playerProfile.User);
        }
    }
}
