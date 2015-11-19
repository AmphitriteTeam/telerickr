var photosController = (function () {

    function all(context) {
        var photos;
        data.photos.get()
            .then(function (resPhotos) {
                photos = resPhotos;
                return templates.get('photos');
            })
            .then(function (template) {
                context.$element().html(template(photos));
            });
    }

    function getById(context) {
        var photo;
        data.photos.getById(this.params['id'])
            .then(function (resPhoto) {
                photo = resPhoto;
                return templates.get('single-photo');
            })
            .then(function (template) {
                context.$element().html(template(photo));
            });
    }

    function add(context) {
        templates.get('share-photos')
            .then(function (template) {
                context.$element().html(template());
                return data.albums.get();
            })
            .then(function (albums) {
                console.log(albums);
                albums.forEach(function (album) {
                    var $option = $('<option>').text(album.Title).attr('value', album.Id);
                    $('#albums-dropdown').append($option);
                });

                $('#btn-photo-add').on('click', function () {
                    var photo = {
                        title: $('#tb-photo-title').val(),
                        imageUrl: $('#tb-photo-url').val(),
                        albumId: $('#albums-dropdown').val()
                    };

                    data.photos.add(photo)
                        .then(function (photo) {
                            toastr.success('Photo added!');
                            context.redirect('#/photos');
                        });
                });
            });
    }

    return {
        all: all,
        add: add,
        getById: getById
    };
}());
