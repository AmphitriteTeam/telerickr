var albumsController = (function() {

    function all(context) {
        var albums;
        data.albums.get()
            .then(function(resAlbums) {
                albums = resAlbums;
                console.log(albums);
                return templates.get('cookies');
            })
            .then(function(template) {
                context.$element().html(template(albums));
                return data.categories.get();
            });
    }

    function add(context) {
        templates.get('share-cookie')
            .then(function(template) {
                context.$element()
                    .html(template());
                return data.categories.get();
            })
            .then(function(categories) {
                $('#tb-cookie-category').autocomplete({
                    source: categories
                });
                $('#btn-cookie-add').on('click', function() {
                    var cookie = {
                        text: $('#tb-cookie-text').val(),
                        category: $('#tb-cookie-category').val(),
                        img: $('#tb-cookie-img').val()
                    };

                    data.cookies.add(cookie)
                        .then(function(cookie) {
                            toastr.success('Cookie added!');
                            context.redirect('#/cookies');
                        });
                });

                $('.reshare-cookie-btn').on('click', function() {
                    var cookie = {
                        text: $('#tb-cookie-text').val(),
                        category: $('#tb-cookie-category').val()
                    };
                });
            });
    }

    return {
        all: all,
        add: add
    };
}());
