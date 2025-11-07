// Call this to register your module to main application
var moduleName = 'virtoCommerce.AiHelper';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider',
        function ($stateProvider) {
        //    $stateProvider
        //        .state('workspace.AiHelperState', {
        //            url: '/ai-helper',
        //            templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
        //            controller: [
        //                'platformWebApp.bladeNavigationService',
        //                function (bladeNavigationService) {
        //                    var newBlade = {
        //                        id: 'blade1',
        //                        controller: 'virtoCommerce.AiHelper.translateController',
        //                        template: 'Modules/$(VirtoCommerce.AiHelper)/Scripts/blades/translate.html',
        //                        isClosingDisabled: true,
        //                    };
        //                    bladeNavigationService.showBlade(newBlade);
        //                }
        //            ]
        //        });
        }
    ])
    .run(['platformWebApp.mainMenuService', '$state',
        function (mainMenuService, $state) {
            //Register module in main menu
        //    var menuItem = {
        //        path: 'browse/ai-helper',
        //        icon: 'fa fa-cube',
        //        title: 'AiHelper',
        //        priority: 100,
        //        action: function () { $state.go('workspace.AiHelperState'); },
        //        permission: 'ai-helper:access',
        //    };
        //    mainMenuService.addMenuItem(menuItem);
        }
    ]);
