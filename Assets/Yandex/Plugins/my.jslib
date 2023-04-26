mergeInto(LibraryManager.library, {

  RateGame: function () {

    ysdk.feedback.canReview()
    .then(({ value, reason }) => {
      if (value) {
        ysdk.feedback.requestReview()
        .then(({ feedbackSent }) => {
          console.log(feedbackSent);
        })
      } else {
        console.log(reason)
      }
    });
  },

  GetLang : function(){
    var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);

    return buffer;
  },

  SaveExtern: function(date){
    var dateString = UTF8ToString(date);
    var myobj = JSON.parse(dateString);
    player.setData(myobj);
    console.log("Save Data!");
  },

  LoadExtern: function(){
    player.getData().then(_date =>{
      const myJSON = JSON.stringify(_date);
      myGameInstance.SendMessage('DataManager', 'LoadData', myJSON);
      console.log("Load Data!");
    });
  },

    ShowAdv : function(){
    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          console.log("---------- closed ADV ------------");
          myGameInstance.SendMessage('GameManager', 'HideADV');
        },
        onError: function(error) {
          // some action on error
          myGameInstance.SendMessage('GameManager', 'HideADV');
        }
        }
        })
  },

    GetHelpLevelExtern : function(){
    ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded! GetHelpLevelExtern');
        },
        onClose: () => {
          console.log('Video ad closed.');
          myGameInstance.SendMessage('GameManager', 'GetLevelHelp');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
          myGameInstance.SendMessage('GameManager', 'HideADV');
        }
      }
      })
      },

    GetHintExtern : function(){
    ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded! GetHintExtern');
        },
        onClose: () => {
          console.log('Video ad closed.');

          myGameInstance.SendMessage('GameManager', 'GetHit');
          window.focus();
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
          myGameInstance.SendMessage('GameManager', 'HideADV');
        }
      }
      })
      }, 

  Auth: function() {
    auth();
  },

  GetData: function() {
    getUserData();
  },
  
  SetData : function(data){
    setUserData(UTF8ToString(data));
  },

  VKShowAdvExtern: function() {
    showFullscrenAd();
  },

  VKRewardAdvExtern: function() {
    showRewardedAd();
  }, 

  VKRewardNextLevelAdvExtern: function() {
    showRewardNextLevelAd();
  }, 

  InviteFriendsExtern: function(){
    invateFriends();
  },

  GoToGroupExtern: function(){
    goToGroup();
  }

});