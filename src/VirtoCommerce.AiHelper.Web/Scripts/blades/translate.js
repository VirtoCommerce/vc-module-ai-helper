angular.module('virtoCommerce.AiHelper')
    .controller('virtoCommerce.AiHelper.translateController', ['$scope', 'virtoCommerce.AiHelper.webApi',
        function ($scope, aiApi) {
            var blade = $scope.blade;
            blade.title = 'aihelper.blades.translate.title';
            blade.languages = ['de-DE', 'fr-FR', 'ar-AR', 'ru-RU'];

            blade.refresh = function () {
                blade.isLoading = false;
            }

            $scope.doTranslate = function () {
                blade.isLoading = true;
                aiApi.translate({
                    text: blade.sourceText,
                    targetLanguage: blade.selectedLanguage
                }, function (data) {
                    blade.resultText = data.result;
                    blade.isLoading = false;
                });
            }

            blade.refresh();
    }]);
