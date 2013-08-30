CoreModule.dataservice = function (Repository) {

    var _UserProfileRepository = new Repository('UserProfile', {

        Details: function (model) {
            return this.FindOne(model, null, 'Details');
        },

        UpdatePicture: function (fileInfo) {
            return this.Update(fileInfo, 'Picture');
        },

    });

    var _CountryRepository = new Repository('Country');
   

    return {
        UserProfile: _UserProfileRepository,
        Country: _CountryRepository
    };

};