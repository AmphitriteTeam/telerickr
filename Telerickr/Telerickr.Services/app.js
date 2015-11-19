(function() {

  var sammyApp = Sammy('#content', function() {

    this.get('#/', function() {
      this.redirect('#/home');
    });
    this.get('#/home', albumsController.all);

    this.get('#/users/register', usersController.register);

    this.get('#/albums', albumsController.all);
    this.get('#/albums/share', albumsController.add);

    this.get('#/photos', photosController.all);
    this.get('#/photos/share', photosController.add);
    this.get('#/photos/:id', photosController.getById);

    this.get('#/my-albums', myAlbumsController.get);

  });

  $(function() {
    sammyApp.run('#/home');

    var $mainNav = $('#main-nav');

    if (data.users.hasUser()) {

      var $shareAlbumItem = $('<li>').attr('id', 'share-album-item');
      var $shareAlbumLink = $('<a>').attr('href', './#/albums/share').text('Share Album');
      $shareAlbumItem.append($shareAlbumLink);

      var $sharePhotoItem = $('<li>').attr('id', 'share-photo-item');
      var $sharePhotoLink = $('<a>').attr('href', './#/photos/share').text('Share Photo');
      $sharePhotoItem.append($sharePhotoLink);

      var $myAlbumsItem = $('<li>').attr('id', 'my-album-item');
      var $myAlbumsLink = $('<a>').attr('href', './#/my-albums').text('My Albums');
      $myAlbumsItem.append($myAlbumsLink);

      $mainNav.append($shareAlbumItem);
      $mainNav.append($sharePhotoItem);

      $('#container-sign-in').addClass('hidden');
      $('#btn-sign-out').on('click', function(e) {
        e.preventDefault();
        data.users.signOut()
          .then(function() {
            document.location = '#/';
            document.location.reload(true);
          });
      });
    } else {
      var $registerItem = $('<li>').attr('id', 'register-item');
      var $registerLink = $('<a>').attr('href', '#/users/register').text('Register');
      $registerItem.append($registerLink);

      $mainNav.append($registerItem);
      $('#container-sign-out').addClass('hidden');
      $('#btn-sign-in').on('click', function(e) {
      	e.preventDefault();
        var user = {
          username: $('#tb-username').val(),
          password: $('#tb-password').val()
        };
        data.users.signIn(user)
          .then(function(user) {
            document.location = '#/';
            document.location.reload(true);
          }, function (err) {
            $('#container-sign-in').trigger("reset");
            toastr.error('Invalid username and/or password!');
          });
      });
    }
  });
}());