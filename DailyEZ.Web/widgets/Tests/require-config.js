var require = {
        baseURL: "",
        paths: {
            'jquery': '../../js/vendor/jquery-1.9.1',
            'jquery.ba-bbq': '../../js/vendor/jquery.ba-bbq',
            'bootstrap': '../../js/vendor/bootstrap',
        },
        shim: {
            jquery: {
                exports: '$',
            },
            'jquery.ba-bbq': ['jquery'],
            'bootstrap': ['jquery'],

        }
};