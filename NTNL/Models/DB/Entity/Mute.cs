using NTNL.Models.DB.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace NTNL.Models.DB.Entity
{
    class Mute
    {
        private String TwitterID;
        private String userID;
        private String Media;
        private String Tweet;
        private String RT;
        private String Favorite;

        public Mute(String TwitterID, String userID, String Media, String Tweet, String RT, String Favorite){
            this.TwitterID = TwitterID;
            this.userID = userID;
            this.Media = Media;
            this.Tweet = Tweet;
            this.RT = RT;
            this.Favorite = Favorite;
        }

        public Mute(MuteDTO dto)
        {
            this.TwitterID = dto.TwitterID;
            this.userID = dto.userID;
            this.Media = dto.Media;
            this.Tweet = dto.Tweet;
            this.RT = dto.RT;
            this.Favorite = dto.Favorite;
        }

        public MuteDTO createDTO()
        {
           var dto = new MuteDTO();
           dto.TwitterID = TwitterID;
           dto.userID = userID;
           dto.Media = Media;
           dto.Tweet = Tweet;
           dto.RT = RT;
           dto.Favorite = Favorite;
           return dto;
        }
    }
}
