var albumsController = (function() {

    function all(context) {
        var albums;
        data.albums.get()
            .then(function(resAlbums) {
                albums = resAlbums;
                return templates.get('albums');
            })
            .then(function(template) {
                context.$element().html(template(albums));
            });
    }

    function add(context) {
        templates.get('share-albums')
            .then(function(template) {
                context.$element().html(template());
            })
            .then(function() {
                $('#btn-album-add').on('click', function () {
                    var album = {
                        title: $('#tb-album-title').val()
                    };

                    data.albums.add(album)
                        .then(function (album) {
                            toastr.success('Album added!');
                            context.redirect('#/albums');
                        });
                });
            });
    }

    return {
        all: all,
        add: add
    };
}());
