using NTNL.Models.DB.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NTNL.Models.DB.Entity
{
    class Mute
    {
        private int ID;
        private String TwitterID;
        private String userID;
        private String Media;
        private String Tweet;
        private String RT;
        private String Favorite;

        public Mute(int ID, String TwitterID, String userID, String Media, String Tweet, String RT, String Favorite){
            this.ID = ID;
            this.TwitterID = TwitterID;
            this.userID = userID;
            this.Tweet = Tweet;
            this.RT = RT;
            this.Favorite = Favorite;
        }

        public Mute(MuteDTO dto)
        {
            this.ID = dto.getID();
            this.TwitterID = dto.getTwitterID();
            this.userID = dto.getUserID();
            this.Media = dto.getMedia();
            this.Tweet = dto.getTweet();
            this.RT = dto.getRT();
            this.Favorite = dto.getFavorite();
        }

        public MuteDTO createDTO()
        {
           MuteDTO dto = new MuteDTO();
           dto.setID(ID);
           dto.setTwitterID(TwitterID);
           dto.setUserID(userID);
           dto.setMedia(Media);
           dto.setTweet(Tweet);
           dto.setRT(RT);
           dto.setFavorite(Favorite);

           return dto;
        }
    }
}
