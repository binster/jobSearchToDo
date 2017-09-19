
//Controller
(function () {
    "use strict";

    angular.module("toDoApp")
        .controller("toDoController", toDoController);
    
    //controller injections
    toDoController.$inject = ["toDoService"];

    function toDoController(toDoService) {

        //admin
        var vm = this;

        //view model
        vm.showTable = true;
        vm.taskList = [];
        vm.data = {};
        vm.$onInit = _init;
        vm.addNewTask = _addNewTask;

        //fold
        function _init() {
            toDoService.getAllTasks()
                .then(_getTaskSuccess, _getTaskFail);
        };

        function _addNewTask() {
            if (vm.data.Task) {
                vm.showTable = false;
            } else {
                console.log("put in a Task Name");
            }
        }

        function _getTaskSuccess(response) {
            console.log(response);
            vm.taskList = response.data.Tasks;
        };

        function _getTaskFail(response) {
            console.log(response);
        }


    }
})();

//Services
(function () {
    "use strict";
    angular.module("toDoApp")
        .service("toDoService", toDoService);

    //service injections
    toDoService.$inject = ["$http"];

    function toDoService($http) {
        //service functions
        this.getAllTasks = _getAllTasks;

        //function definitions
        function _getAllTasks() {
            var settings = {
                url: "/api/toDo",
                method: "GET",
                cache: false,
                responseType: "json",
            };
            return $http(settings);
        };
    }
})();