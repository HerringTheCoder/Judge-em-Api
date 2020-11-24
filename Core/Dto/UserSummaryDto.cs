using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dto
{
    public class UserSummaryDto
    {
        public string PlayerProfileId { get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int? SummaryId { get; set; }
        public DateTime FinishedAt { get; set; }
    }
}
