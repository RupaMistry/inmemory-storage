'use strict';

keyValueStoreApp.factory('KeyValueStoreService', ['$http', '$q', 'apiConstants', function ($http, $q, apiConstants) {

    return {

        getKeyValue: function (nameSpace, key) {
            return $http.get(apiConstants.apiUrl + nameSpace + '/' + key)
                .then(
                    function (response) {
                        return response.data;
                    },
                    function (errResponse) {
                        return $q.reject(errResponse);
                    }
                );
        },

        getAllKeyValues: function (nameSpace) {
            return $http.get(apiConstants.apiUrl + nameSpace)
                .then(
                    function (response) {
                        return response.data;
                    },
                    function (errResponse) {
                        return $q.reject(errResponse);
                    }
                );
        },

        saveKeyValue: function (keyValueDataModel) {
            return $http.put(apiConstants.apiUrl, keyValueDataModel)
                .then(
                    function (response) {
                        return response.data;
                    },
                    function (errResponse) {
                        return $q.reject(errResponse);
                    }
                );
        },

        deleteKeyValue: function (namespace, key) {
            return $http.delete(apiConstants.apiUrl + namespace + '/' + key + '/')
                .then(
                    function (response) {
                        return response.data;
                    },
                    function (errResponse) {
                        return $q.reject(errResponse);
                    }
                );
        }

    };
}]);