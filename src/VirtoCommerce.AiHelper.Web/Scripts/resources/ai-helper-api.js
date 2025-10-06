angular.module('virtoCommerce.AiHelper')
    .factory('virtoCommerce.AiHelper.webApi', ['$resource', function ($resource) {
        return $resource('api/aihelper', null, {
            translate: { method: 'POST', url: 'api/aihelper/translate' },
        });
    }]);
