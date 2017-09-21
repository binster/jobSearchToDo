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
        vm.editTask = _editTask;
        vm.deleteTask = _deleteTask;
        vm.clearTask = _clearTask;

        //for ui bootstraps calendar
        vm.openCal = _openCal;

        //fold
        function _init() {
            toDoService.getAllTasks()
                .then(_getTaskSuccess, _getTaskFail);
        };

        function _addNewTask() {
            vm.index = -1;
            if (vm.data.Task) {
                vm.showForm = true;
                vm.isNewTask = true;
            } else {
                console.log("put in a Task Name");
            }
        }

        function _submitTask() {    
            if (vm.index == -1) {
                //give default due date of tomorrow
                //if (!vm.data.DueBy) {
                //    vm.data.DueBy = new Date();
                //}
                toDoService.postTask(vm.data)
                    .then(_postSuccess, _postFail);
            } else {
                toDoService.updateTask(vm.data)
                    .then(_updateSuccess, _updateFail);
            }
        }

        function _editTask(index, task) {
            vm.index = index;
            vm.data = task;
            vm.showForm = true;

        }

        function _deleteTask(index, taskId) {
            console.log(taskId);
            vm.index = index;
            toDoService.deleteTask(taskId)
                .then(_deleteSuccess, _deleteFail);
        }

        function _clearTask() {
            vm.data = {};
            vm.index = -1;
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

        function _updateSuccess(response) {
            console.log(response);
            vm.taskList[vm.index] = vm.data;
            vm.statusMessage = "updated task successfully";
            _clearTask();
        }

        function _updateFail(response) {
            console.log(response);
            vm.statusMessage = "update failed";
        }

        function _getTaskSuccess(response) {
            console.log(response);
            vm.taskList = response.data.Tasks;
        };

        function _getTaskFail(response) {
            console.log(response);
            vm.statusMessage = "there was an error in the database";
        }

        function _deleteSuccess(response) {
            console.log(response);
            vm.taskList.splice(vm.index, 1);
            vm.index = -1;
            vm.statusMessage = "successful removal of task";        
        }

        function _deleteFail(response) {
            console.log(response);
            vm.statusMessage = "delete failed";
        }

        //for ui bootstraps calendar

        function _openCal() {
            vm.popupCal = true;
        }
    }
    //things to add : form validation
    //toastr or popup to user to ensure successful call
    //fewer buttons
    //order by date/priority
    //when making form, priority is a string when edit mode its an int
    //change datetime to string?
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
        this.updateTask = _updateTask;
        this.deleteTask = _deleteTask;

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

        //update Task
        function _updateTask(task) {
            var settings = {
                url: "/api/toDo/" + encodeURI(task.Id),
                method: "PUT",
                cache: false,
                contentType: "application/json",
                data: task
            };
            return $http(settings);
        }

        //delete Task
        function _deleteTask(taskId) {
            var settings = {
                url: "/api/toDo/" + encodeURI(taskId),
                method: "DELETE",
                cache: false,
                responseType: "json"
            };
            return $http(settings);
        }
    }
})();