var usersController = function() {
  function register(context) {
    templates.get('register')
      .then(function(template) {
        context.$element().html(template());

        $('#btn-register').on('click', function() {
          var user = {
            email: $('#tb-reg-username').val(),
            password: $('#tb-reg-pass').val(),
            confirmPassword: $('#tb-reg-confirmPass').val()
          };

          data.users.register(user)
            .then(function() {
              toastr.success('User ' + user.username + ' registered! You can now login!');
              context.redirect('#/');
            });
        });
      });
  }

  return {
    register: register
  };
}();