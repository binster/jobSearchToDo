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
        vm.statusMessage = "";
        vm.showForm = false;
        vm.taskList = [];
        vm.data = {};
        vm.index = -1;
        vm.$onInit = _init;
        vm.addNewTask = _addNewTask;
        vm.submitTask = _submitTask;
        vm.deleteTask = _deleteTask;
        vm.clearTask = _clearTask;

        //fold
        function _init() {
            toDoService.getAllTasks()
                .then(_getTaskSuccess, _getTaskFail);
        };

        function _addNewTask() {
            if (vm.data.Task) {
                vm.showForm = true;
                vm.isNewTask = true;
            } else {
                console.log("put in a Task Name");
            }
        }

        function _submitTask() {
            
            if (vm.index == -1) {
                if (!vm.data.DueBy) {
                    vm.data.DueBy = null;
                }
                toDoService.postTask(vm.data).then(_postSuccess, _postFail);
            }
        }

        function _deleteTask(taskId) {
            console.log(taskId);
        }

        function _clearTask() {
            vm.data = {};
            vm.showForm = false;
            vm.isNewTask = false;
        }

        function _postSuccess(response) {
            console.log(response);
            debugger;
            vm.data.Id = response.data.item;            
            vm.taskList.push(vm.data);
            vm.statusMessage = "post success";
            _clearTask();
        }

        function _postFail(response) {
            vm.statusMessage = "there was an error while uploading your post";
            console.log(response);
        }

        function _getTaskSuccess(response) {
            console.log(response);
            vm.taskList = response.data.Tasks;
        };

        function _getTaskFail(response) {
            console.log(response);
            vm.statusMessage = "there was an error in the database";
        }


    }
    //things to add : form validation
    //toastr or popup to user to ensure successful call
    //fewer buttons
    //order by date/priority
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
        this.postTask = _postTask;

        //function definitions

        //get All tasks
        function _getAllTasks() {
            var settings = {
                url: "/api/toDo",
                method: "GET",
                cache: false,
                responseType: "json"
            };
            return $http(settings);
        };

        //post Task
        function _postTask(taskData) {
            var settings = {
                url: "/api/toDo",
                method: "POST",
                cache: false,
                contentType: "application/json",
                data : taskData
            }
            return $http(settings);
        }
    }
})();