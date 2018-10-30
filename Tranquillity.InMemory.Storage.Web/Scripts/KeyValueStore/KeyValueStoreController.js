keyValueStoreApp.controller("KeyValueStoreController", keyValueStoreController)

keyValueStoreController.$inject = ["$scope", "$http", "$filter", "$route", "KeyValueStoreService"];

function keyValueStoreController($scope, $http, $filter, $route, keyValueStoreService) {
    $scope.KeyValueDataModel = {
        NameSpace: "",
        Key: "",
        Value: {
            ID: 0,
            Name: "",
            Description: "",
            Price: '',
            Unit: '',
        }
    }

    $scope.PartialView = {
        ShowAddNew: false,
        ShowSearchByNamespace: false,
        ShowSearchByNamespaceAndKey: false
    }

    $scope.KeyValueStoreList = [];

    $scope.FilterParams = {
        NameSpace: "",
        Key: ""
    }

    $scope.onSaveClick = function (keyValueData) {

        if (validate(keyValueData)) {
            var keyValueDataModel = {
                NameSpace: keyValueData.NameSpace,
                Key: keyValueData.Key,
                Value: keyValueData.Value
            };
            keyValueStoreService.saveKeyValue(keyValueDataModel)
                .then(
                    function (response) {
                        if (response == "Success") {
                            $scope.AddUpdateResponseMessage = "Key-Value for product saved successfully!"
                        }
                    },
                    function (errResponse) {
                        $scope.AddUpdateResponseMessage = "Error occured. Key-Value for product not saved!"
                    }
                );

        } else
            $scope.AddUpdateResponseMessage = "All feilds are mandatory";
    }

    $scope.onFilterByNameSpaceClick = function () {

        $scope.GetAllResponseMessage = "";

        if ($scope.FilterParams.NameSpace.trim() == '' || $scope.FilterParams.Key.trim() == '') {
            $scope.GetResponseMessage = "Enter a valid NameSpace and Key."
            return;
        } else
            getByNameSpaceAndKey();
    }

    $scope.getAllByNameSpace = function (nameSpace) {
        
        $scope.GetAllResponseMessage = "";

        if (nameSpace == '' || nameSpace === undefined) {
            $scope.GetAllResponseMessage = "Enter a Valid NameSpace.";
            $scope.DeleteResponseMessage = "";
            return;
        }

        $scope.KeyValueStoreList = [];

        keyValueStoreService.getAllKeyValues(nameSpace)
            .then(
                function (data) {
                    if (data.length <= 0) {
                        $scope.GetAllResponseMessage = "Given NameSpace does not exists!";
                        $scope.DeleteResponseMessage = "";
                        return;
                    }
                    data.forEach(function (keyValueData) {
                        var keyValueDataModel = {}
                        var keyformatData = keyValueData.Key.split('_');
                        keyValueDataModel.NameSpace = keyformatData[0];
                        keyValueDataModel.Key = keyformatData[1];
                        keyValueDataModel.Value = keyValueData.Value;
                        $scope.KeyValueStoreList.push(keyValueDataModel);
                    });
                },
                function (errResponse) {
                    $scope.GetAllResponseMessage = errResponse.statusText;
                }
            );
    }

    $scope.onDeleteClick = function (namespace, key) {

        var keyValueData = $.param({
            NameSpace: namespace,
            Key: key
        });

        keyValueStoreService.deleteKeyValue(namespace, key)
            .then(
                function (data) {
                    if (data == true) {

                        $scope.DeleteResponseMessage = "Record for Key: " + key + " deleted successfully!";
                        if ($scope.KeyValueStoreList.length - 1 == 0) {
                            $scope.KeyValueStoreList = [];
                        } else {
                            $scope.getAllByNameSpace(namespace);
                        }

                    }
                },
                function (errResponse) {
                    $scope.DeleteResponseMessage = "Error occured. Failed to delete the item."
                }
            );
    }

    function getByNameSpaceAndKey() {
        keyValueStoreService.getKeyValue($scope.FilterParams.NameSpace, $scope.FilterParams.Key)
            .then(
                function (data) {
                    $scope.ValueResponse = data;
                    $scope.GetResponseMessage = "";
                },
                function (errResponse) {
                    $scope.GetResponseMessage = "Given NameSpace-Key item does not exists!";
                }
            );

    }

    function validate(keyValueData) {
        if (keyValueData.NameSpace.trim() != '' && keyValueData.Key.trim() != '' &&
            keyValueData.Value.Name.trim() != '' && keyValueData.Value.Description.trim() != '' &&
            keyValueData.Value.Price != null && keyValueData.Value.Unit != null)
            return true;
        else
            return false;
    }
}