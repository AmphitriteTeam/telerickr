var myAlbumsController = (function() {

    function get(context) {
        var albums = [];
        data.albums.getMyAlbums()
            .then(function(resAlbum) {
                albums.push(resAlbum);

                if (data.users.hasUser()) {
                    albums = _.map(albums, controllerHelpers.fixUser);
                }

                albums = _.chain(albums)
                    .map(controllerHelpers.fixDate).value();

                return templates.get('my-album');
            })
            .catch(function(err) {
                toastr.error(err);
            });
    }

    return {
        get: get
    };
})();
