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
        },
        onError: function(error) {
          // some action on error
        }
        }
        })
  },

});