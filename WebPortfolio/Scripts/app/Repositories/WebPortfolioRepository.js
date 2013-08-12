var WebPortfolioRepository = function (Repository) {
    var _UserProfileRepository = new Repository('UserProfile');

    _UserProfileRepository.Details = function (model) {
        _UserProfileRepository.FindOne(model, null, 'Details');
    };




    return {

        UserProfile : _UserProfileRepository

    };
};