var data = (function() {
    var LOCAL_STORAGE_USERNAME_KEY = 'x-username',
        LOCAL_STORAGE_AUTHKEY_KEY = 'Authorization';

    var USERNAME_CHARS = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890_.",
        USERNAME_MIN_LENGTH = 6,
        USERNAME_MAX_LENGTH = 30;

    var ALBUM_TITLE_MIN_LENGTH = 6,
        ALBUM_TITLE_MAX_LENGTH = 30

    /* Users */

    function register(user) {
        var error = validator.validateString(user.username, USERNAME_MIN_LENGTH, USERNAME_MAX_LENGTH, "name", USERNAME_CHARS);

        if (error) {
            toastr.error(error.message);
            return Promise.reject(error.message);
        }

        var reqUser = {
            password: user.password,
            confirmPassword: user.confirmPassword
        };

        return jsonRequester.post('api/account/register', {
                data: reqUser
            })
            .then(function(resp) {
                return {
                    username: resp.result.username
                };
            });
    }


    function signIn(user) {
        var error = validator.validateString(user.username, USERNAME_MIN_LENGTH, USERNAME_MAX_LENGTH, "name", USERNAME_CHARS);

        if (error) {
            toastr.error(error.message);
            return Promise.reject(error.message);
        }

        var reqUser = {
            username: user.username,
            password: user.password,
            grant_type: "password"
        };

        var options = {
            data: reqUser
        };

        return jsonRequester.put('api/account/login', options)
            .then(function(resp) {
                localStorage.setItem(LOCAL_STORAGE_USERNAME_KEY, user.username);
                localStorage.setItem(LOCAL_STORAGE_AUTHKEY_KEY, resp.access_token);
                return user;
            });
    }

    function signOut() {
        var promise = new Promise(function(resolve, reject) {
            localStorage.removeItem(LOCAL_STORAGE_USERNAME_KEY);
            localStorage.removeItem(LOCAL_STORAGE_AUTHKEY_KEY);
            resolve();
        });
        return promise;
    }

    function hasUser() {
        return !!localStorage.getItem(LOCAL_STORAGE_USERNAME_KEY) &&
            !!localStorage.getItem(LOCAL_STORAGE_AUTHKEY_KEY);
    }

    function albumsGet() {
        var options = {
            headers: {
                'Authorization': "Bearer " + localStorage.getItem(LOCAL_STORAGE_AUTHKEY_KEY)
            }
        };
        return jsonRequester.get('api/albums', options)
            .then(function(res) {
                console.log(res);
                return res;
            });
    }

    function getMyAlbums() {
        var options = {
            headers: {
                'Authorization': "Bearer " + localStorage.getItem(LOCAL_STORAGE_AUTHKEY_KEY)
            }
        };

        return jsonRequester.get('api/my-albums', options)
            .then(function(res) {
                return res.result;
            });
    }

    function albumsAdd(album) {

        var error = validator.validateString(album.title, ALBUM_TITLE_MIN_LENGTH, ALBUM_TITLE_MAX_LENGTH, "album title");
        if (error) {
            toastr.error(error.message);
            return Promise.reject('Title ' + error.message);
        }

        var options = {
            data: album,
            headers: {
                'x-auth-key': localStorage.getItem(LOCAL_STORAGE_AUTHKEY_KEY)
            }
        };

        return jsonRequester.post('api/albums', options)
            .then(function(resp) {
                return resp.result;
            });
    }

    return {
        users: {
            signIn: signIn,
            signOut: signOut,
            register: register,
            hasUser: hasUser,
            get: usersGet,
            getUserById: getUserById
        },
        albums: {
            get: albumsGet,
            getMyAlbums: getMyAlbums,
            add: albumsAdd,
            like: albumsLike,
            dislike: albumsDislike
        }
    };
}());
