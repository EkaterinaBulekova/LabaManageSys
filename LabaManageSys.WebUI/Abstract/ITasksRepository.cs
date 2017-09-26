﻿using System.Collections.Generic;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.Abstract
{
    public interface ITasksRepository
    {
        IEnumerable<TaskModel> Tasks { get; }

        IEnumerable<TagModel> Tags { get; }

        int GetTasksCount(FilterModel filter);

        IEnumerable<TaskModel> GetTasksByFilter(FilterModel filterModel, int page, int pageSize);

        IEnumerable<TaskModel> GetTasksByTags(List<string> tags, int page, int pageSize);

        IEnumerable<TaskModel> GetTasksByTag(TagModel tag, int page, int pageSize);

        TaskModel TaskDelete(int taskId);

        TaskModel GetTaskById(int taskId);

        TaskModel GetTaskByName(string name);

        FilterListsModel GetFilterLists();

        void TaskUpdate(TaskModel task);
        
        ResultModel TaskAddAll(IEnumerable<TaskModel> tasks);

        double GetAvgRatingByTaskId(int taskId);

        RatingModel GetRatingByTaskUser(string userName, int taskId);

        IEnumerable<RatingModel> GetRatingsByTask(int taskId, int page, int pageSize);

        void RatingDelete(int ratingId);

        void RatingUpdate(RatingModel rating);

        int[] GetRatingsByTaskPercents(int taskId);

        int GetRatingsByTaskCount(int taskId);
    }
}
