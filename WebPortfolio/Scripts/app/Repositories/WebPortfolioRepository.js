var WebPortfolioRepository = function (Repository) {

    var _UserProfileRepository = new Repository('UserProfile', {

        Details: function (model) {
            return this.FindOne(model, null, 'Details');
        }

    });

    var _CountryRepository = new Repository('Country');
   

    return {
        UserProfile: _UserProfileRepository,
        Country: _CountryRepository
    };

};