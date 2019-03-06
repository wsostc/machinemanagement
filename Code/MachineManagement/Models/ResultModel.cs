using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineManagement.Models
{
    public class ResultModel
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public string JsonObject { get; set; }
        public int TotalPages { get; set; }

        public ResultModel()
        {
            this.IsValid = true;
            this.ErrorMessage = string.Empty;
        }

        public void SetFailed(string message)
        {
            this.IsValid = false;
            this.ErrorMessage = message;
        }

        public void SetTotalPages(int recordCount, int pageSize)
        {
            int totalPages = recordCount / pageSize;

            if (totalPages * pageSize < recordCount)
                totalPages++;

            if (totalPages <= 0)
                totalPages = 1;

            this.TotalPages = totalPages;
        }

        public int GetCurRowNum(int currentPage, int pageSize)
        {
            if (currentPage < 1) currentPage = 1;
            if (currentPage > this.TotalPages) currentPage = this.TotalPages;

            return (currentPage - 1) * pageSize;
        }
    }
}