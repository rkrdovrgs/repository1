var WebPortfolioRepository = function (Repository) {

    var _UserProfileRepository = new Repository('UserProfile', {

        Details: function (model) {
            return this.FindOne(model, null, 'Details');
        }

    });
   

    return {
        UserProfile: _UserProfileRepository
    };

};