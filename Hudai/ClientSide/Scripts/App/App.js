angular.module('MyApp', ['ngGrid']).controller('MainController', function($scope){

    $scope.superHeroes = [
        { name: "Tony Stark", alterEgo: "IronMan", Weight: '80'},
        { name: "Thor Odinson", alterEgo: "Thor", Weight: '80' },
        { name: "Miral Sarwar", alterEgo: "MiralDaBOSS", Weight: '90' },
        { name: "Najim Ahmed", alterEgo: "BC", Weight: '70' },
        { name: "Miral Sarwar", alterEgo: "MC", Weight: '90'}
    ];

    $scope.selected = [];

    $scope.gridOps = {
        data: 'superHeroes',
        showGroupPanel: true,
        selectedItems: $scope.selected,
        multiSelect: false,
        columnDefs: [
            { field: "name", displayName: "Name" },
            { field: "alterEgo", displayName: "Character" },
            { field: "Weight", displayName: "Mass"}
        ]
    };
});